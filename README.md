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
To see the API specification, navigate to: http://localhost:5280/openapi/v1.json


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
And we can also navigate to the API specification: http://localhost:5135/openapi/v1.json


## Generating an API
We can also generate an API. To do this however we will need to specify an OpenAPI specification first. With the samples above we navigated to this specification. We can save this and use this as a basis for our specification. But we could also start from scratch. See the [OpenAPI specification](https://swagger.io/specification/) to see how this works. 
==Note that the OpenAPI specification can be written in json and yaml files==


### NSwag
[Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-8.0&tabs=visual-studio)
[NSwag Documentation](https://github.com/RicoSuter/NSwag)
When we have our OpenAPI specification we are able to generate our API. The first tool we are going to examine is NSwag.

### Installing NSwag
To install the NSwag cli tool. We will use npm.

```powershell
npm install nswag -g
```

### Setting up the project
First we will create a directory and generate a NSwag file:

```powershell
mkdir Hogwarts.Api.NSwagApi
cd Hogwarts.Api.NSwagApi
nswag new
```

This will create a `nswag.json` file.

In the `nswag.json` file we will only use the `openApiToCSharpController` codegenerator for now. The other generators are use to generate API clients. Which we will cover in a later chapter.

For now change the `url` field from `documentGenerator/fromDocument` to your OpenAPI specification file. I created mine in a folder `Hogwarts.Api.Contracts` so my `url` is set to: `"../Hogwarts.Api.Contracts/openapi.yml"`. Next make sure to set the field `output` in your codegenerator settings. I have set mine to `"Hogwarts.Api.NSwagApi.g.cs"` \*.g.\* is used to show the file is generated.

### Generating the API
That should be enough to generate the API. To generate the api we only have to run a simple command:
```powershell
nswag run .\Hogwarts.Api.NSwagApi\nswag.json
```

### OpenAPI Generator

# Create OpenAPI spec
## .NET minimal API
## NSwag
## Swashbuckle
## TypeSpec
## 

# Create a client
## NSwag
## Openapi Generator
## Kiota

# OpenAPI UI
## Swagger
## Scalar

# TODO
- NSwag client generation
- 
- Define what you should know about defining the nswag file
- 
- Add http test requests
- Add scalar instead of swagger ui
- Try out some complex models
- Figure out how this all would work in ci/cd
- Testing the Kiota generated client(s)
- 


## Generating a client
### NSwag
```powershell
nswag new
```
Edit the generated nswag.json

```powershell
nswag run
```

Navigate to http://localhost:5194/openapi/v1.json

# Swagger UI
[Documentation](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
[Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-8.0)

```powershell
dotnet add .\Hogwarts.Net.Api\ package Swashbuckle.AspNetCore
```

Navigate to http://localhost:5194/swagger

# Scalar UI
[Documentation](https://github.com/scalar/scalar/blob/main/packages/scalar.aspnetcore/README.md)
```powershell
dotnet add .\Hogwarts.Net.Api\ package Scalar.AspNetCore
```

Nvaigate to http://localhost:5194/scalar/v1

# NSwag 
[Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-8.0&tabs=visual-studio)

```powershell
dotnet add .\Hogwarts.Net.Api\ package NSwag.AspNetCore
```

# TypeSpec
[Documentation](https://typespec.io/)

## Starting new project
```powershell
npm install -g @typespec/compiler
tsp init
tsp install
```

## Compiling contracts
```powershell
npm install -g @typespec/compiler
tsp install
tsp compile .\Hogwarts.Api.TypeSpec.Contracts\ --output-dir .\tsp-output\
```

## Update contracts
```powershell
tsp compile .\Hogwarts.Api.TypeSpec.Contracts\ --output-dir .\tsp-output\
```


# Kiota
[Documentation](https://learn.microsoft.com/en-us/openapi/kiota/overview)


## Install
```powershell
dotnet tool install --global Microsoft.OpenApi.Kiota
dotnet new classlib -o Hogwarts.Api.KiotaClient
```

## Create client using Kiota

### Using typespec contracts
```powershell
kiota generate -l CSharp -n Hogwarts.Api.Client -d '.\tsp-output\@typespec\openapi3\openapi.yaml' -o .\Hogwarts.Api.KiotaClient\
```

# NSwag
[Documentation](https://github.com/RicoSuter/NSwag)

## Install NSwag

## Start new NSwag project
```powershell
nswag new
```


## Generating using NSwag
With NSwag we can define the generations to be done using the nswag.json file which is generated when using `nswag new`
Noteworthy generators: `openApiToCSharpController` and `openApiToCSharpClient` to help generating the api server and client.
Remember that when the output is not set, no generation will be done.

For now we need to run it from the API folder. This due to a bug in the NSwag generator:
```powershell
nswag run /runtime:net80
```

When the bug is resolved we should be able to run it like this:
```powershell
nswag run .\Hogwarts.Api\nswag.json /runtime:Net80
```

## Generating only the controller (deprecated):
```powershell
nswag openapi2cscontroller /input:.\tsp-output\@typespec\openapi3\openapi.yaml /output:Hogwarts.Api.Contracts
```

# openapi-generator
[Documentation](https://openapi-generator.tech)

## Install openapi-generator
```powershell
npm install @openapitools/openapi-generator-cli -g
```

## Generate API
```powershell
openapi-generator-cli generate -i '.\Hogwarts.Api.Contracts\tsp-output\@typespec\openapi3\openapi.yaml' -g aspnetcore -o Hogwarts.OpenApiGenerator.Api 
.\Hogwarts.Api\build.bat
```

## Generate Client
```powershell
openapi-generator-cli generate -i '.\Hogwarts.Api.Contracts\tsp-output\@typespec\openapi3\openapi.yaml' -g csharp -o Hogwarts.Api.OpenApiGeneratorClient
```


# Openapi tools
[Documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/openapi-tools?view=aspnetcore-8.0)

## Install
```powershell
dotnet tool install -g Microsoft.dotnet-openapi
```