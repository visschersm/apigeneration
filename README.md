This repository I use to research some different kinds to setup and develop API's and clients for .NET.
Using different kind of tools to generate source and specifications.


# Create API
This part describes how we can use different .NET tools to create APIs.


## .NET minimal API
[Minimal API Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio)
[.NET 9 OpenAPI Documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-8.0&tabs=visual-studio)
The first way to create an API is using the .NET default API generation. Minimal apis were [introduced in .NET 6](https://medium.com/net-newsletter-by-waseem/ep-27-introduction-to-minimal-apis-in-net-d575b7f0d4d4) as a more lightweight way to define your API as opposed to controllers.


### Creating the project
To create a minimal api project simply run the following command:

```powershell
dotnet new webapi -o Hogwarts.Api.NetMinimalApi
```

This will generate all the setup code you will need.


### Running the API
To start the API run the following command:

```powershell
dotnet run --project .\Hogwarts.Api.NetMinimalApi\
```

This will start the API on a port defined in the `properties/launchsettings.json` file.
This will be plotted in the output.


### API Specification
To see the API specification, navigate to: http://localhost:\<port\>/openapi/v1.json

### Adding logic
<!-- @todo: describe how you would add logic to this setup. -->

## .NET Controllers
[Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-8.0)
If you would rather use the previous version of defining your API using controllers this is still possible.


### Creating the project
To create an API using controllers simply provide the create command with the `-controllers` flag. Like so:

```powershell
dotnet new webapi -o Hogwarts.Api.NetControllersApi -controllers
```

This will generate the setup you will need just as the minimal api creation would. The main difference is that now a controllers folder is generated as well. The endpoints are defined over there.


### Running the API
Just like with the minimal API we can run the controller API:

```powershell
dotnet run --project .\Hogwarts.Api.NetControllersApi\
```

### OpenAPI Specification
And we can also navigate to the API specification: http://localhost:\<port\>/openapi/v1.json


### Adding logic
<!-- @todo: describe how you would add logic to this setup. -->


## Generating an API
We can also generate an API. To do this however we will need to specify an OpenAPI specification first. With the samples above we navigated to this specification. We can save this and use this as a basis for our specification. But we could also start from scratch. See the [OpenAPI specification](https://swagger.io/specification/) to see how this works. 

```
Note that the OpenAPI specification can be written in json and yaml files
```


### NSwag
[Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-8.0&tabs=visual-studio)
[NSwag Documentation](https://github.com/RicoSuter/NSwag)
When we have our OpenAPI specification we are able to generate our API. The first tool we are going to examine is NSwag.

#### Installing NSwag
To install the NSwag cli tool. We will use npm.

```powershell
npm install nswag -g
```

#### Setting up the project
First we will create a dotnet webapi controller project and generate a NSwag file:

```powershell
dotnet new webapi -o Hogwarts.Api.NSwagApi -controllers
cd Hogwarts.Api.NSwagApi
nswag new
```

This will create a `nswag.json` file.

In the `nswag.json` file we will only use the `openApiToCSharpController` codegenerator for now. The other generators are use to generate API clients. Which we will cover in a later chapter.

For now change the `url` field from `documentGenerator/fromDocument` to your OpenAPI specification file. I created mine in a folder `Hogwarts.Api.Contracts` so my `url` is set to: `"../Hogwarts.Api.Contracts/openapi.yml"`. Next make sure to set the field `output` in your codegenerator settings. I have set mine to `"Hogwarts.Api.NSwagApi.g.cs"` \*.g.\* is used to show the file is generated.

#### Generating the API
That should be enough to generate the API. To generate the api we only have to run a simple command:
```powershell
nswag run .\Hogwarts.Api.NSwagApi\nswag.json
```

To use our generated controllers we derive our implementations from the generated controller. I added a studentController and derived it from IController. Do not forget to register this in your dependency injection.
```csharp
public class StudentController : IController
{
    public Task<ICollection<Student>> GetStudentsAsync()
    {
        return Task.FromResult<ICollection<Student>>(new Student[]
        {
            new Student
            {
                Id = 1,
                FirstName = "Harry",
                Surname ="Potter"
            },
            new Student
            {
                Id = 2,
                FirstName = "Ronald",
                Surname = "Weasley"
            },
            new Student
            {
                Id = 3,
                FirstName = "Hermione",
                Surname = "Granger"
            }
        });
    }
}
```

```csharp
builder.Services.AddTransient<IController, StudentController>();
```

#### Updating the generated contracts
To update the models and controllers we just run the command again:
```powershell
nswag run .\Hogwarts.Api.NSwagApi\nswag.json
```

This will re-generated the routes and models but since we create implementations based on interfaces, the implementations will keep working. (as long as there are no breaking changes in the updated contracts)

<!-- @todo: describe how to generate separate controllers per operationId -->

#### Adding logic
<!-- @todo: describe how you would add logic to this setup. -->


### OpenAPI Generator
[Documentation](https://openapi-generator.tech)
Next we are going to look at a tool called `OpenAPI Generator`.

#### Install OpenAPI Generator
To install the tool we use npm:

```powershell
npm install @openapitools/openapi-generator-cli -g
```

#### Generate API
To generate the API we run the following command:

```powershell
openapi-generator-cli generate -i '.\Hogwarts.Api.Contracts\openapi.yml' -g aspnetcore -o Hogwarts.Api.OpenAPIGeneratorApi 
.\Hogwarts.Api.OpenAPIGeneratorApi\build.bat
```

This will create a lot more files than NSwag does. At a later time I will extend this document to provide some insights on what these files are and how we would alter the generation process.

<!--
@todo process this
- Altered the use urls in the program.cs
- build.bat uses -p which is deprecated.
- What are all the files that are generated?
- How do we change the naming?
- Why does the docker command not work?
- What is the wwwroot for?
- How would we add our own logic to this?
-->

#### Running the API
The source code of our API is generated under the `src/Org.OpenAPITools` to run this we can run the command and set the `--project` flag to the project:

```powershell
dotnet run --project src\Org.OpenAPITools\Org.OpenAPITools.csproj
```

#### Updating the contracts
<!-- @todo: describe how to update the contracts -->

#### Adding logic
<!-- @todo: describe how you would add logic to this setup. -->


# Create OpenAPI spec
As described before, an OpenAPI specification can be created by writing it by hand using the [OpenAPI Specification](https://swagger.io/specification/) but this might be a bit teadious. We can also generate the specification. The next part of this document describes a couple of tools that can help you with this.


## .NET minimal API
<!-- @todo: describe how minimal apis generate the openapi specification -->

## .NET Controller API
<!-- @todo: describe the difference between the minimal and the controller api specification. -->

## NSwag
<!-- @todo: describe how NSwag can be used to generate an OpenAPI Specification.
[Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-8.0&tabs=visual-studio)

```powershell
dotnet add .\Hogwarts.Net.Api\ package NSwag.AspNetCore
``` 
-->

## Swashbuckle
<!-- @todo: describe how to generate an openapi specification using swashbuckle -->

## TypeSpec
[Documentation](https://typespec.io/)

### Starting new project
```powershell
npm install -g @typespec/compiler
tsp init
tsp install
```

### Compiling contracts
```powershell
npm install -g @typespec/compiler
tsp install
tsp compile .\Hogwarts.Api.TypeSpec.Contracts\ --output-dir .\tsp-output\
```

### Update contracts
```powershell
tsp compile .\Hogwarts.Api.TypeSpec.Contracts\ --output-dir .\tsp-output\
```

# Create a client
## NSwag
<!-- @todo: describe how to generate a client using NSwag -->

## Openapi Generator
<!-- @todo: describe how to generate a client using OpenAPI Generator
 # OpenAPI Generator
## Generate Client
```powershell
openapi-generator-cli generate -i '.\Hogwarts.Api.Contracts\tsp-output\@typespec\openapi3\openapi.yaml' -g csharp -o Hogwarts.Api.OpenApiGeneratorClient
``` -->
## Kiota
[Documentation](https://learn.microsoft.com/en-us/openapi/kiota/overview)


### Install
To install Kiota run the following command:
```powershell
dotnet tool install --global Microsoft.OpenApi.Kiota
```

### Create client using Kiota
To create a client project we first create a dotnet classlib project by running the following command:
```powershell
dotnet new classlib -o Hogwarts.Api.KiotaClient
```

The client is then generated by running the following kiota command:
```powershell
kiota generate -l CSharp -d .\Hogwarts.Api.Contracts\openapi.yml -o .\Hogwarts.Api.KiotaClient\
```

### Updating the client
To update the generated contracts run the following command:
```powershell
kiota update -o .\Hogwarts.Api.KiotaClient\
```


# OpenAPI UI

## Swagger
[Documentation](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
[Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-8.0)

```powershell
dotnet add .\Hogwarts.Net.Api\ package Swashbuckle.AspNetCore
```

Navigate to http://localhost:<port>/swagger


## Scalar
[Documentation](https://github.com/scalar/scalar/blob/main/packages/scalar.aspnetcore/README.md)
```powershell
dotnet add .\Hogwarts.Net.Api\ package Scalar.AspNetCore
```

Navigate to http://localhost:<port>/scalar/v1

# TODO
- Define what you should know about defining the nswag file
- Add http test requests
- Try out some complex models
- Figure out how this all would work in ci/cd
- Testing the Kiota generated client(s)
- Split the readme files in shorted files.

<!-- @todo: research openapi tools
# Openapi tools
[Documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/openapi-tools?view=aspnetcore-8.0)

## Install
```powershell
dotnet tool install -g Microsoft.dotnet-openapi
```
 -->

 <!-- @todo: research other Microsoft OpenAPI tools:
 [OpenAPI Tools](https://github.com/microsoft/OpenAPI.NET) 
 ## Hidi
 [Documentation](https://github.com/microsoft/OpenAPI.NET/blob/vnext/src/Microsoft.OpenApi.Hidi/readme.md)
 ```powershell
 dotnet tool install --global Microsoft.OpenApi.Hidi
 ```
 -->
