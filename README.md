A project using typespec and kiota to create and api

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


# Create API
## .NET minimal API
## Generating an API
### NSwag
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




# dotnet minimal api
[Minimal API Documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio)
[.NET 9 OpenAPI Documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-8.0&tabs=visual-studio)

## Creation
```powershell
dotnet new webapi -o Hogwarts.Net.Api
```

## Running the API
```powershell
dotnet run --project .\Hogwarts.Net.Api\Hogwarts.Net.Api.csproj
```

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
```powershell
winget install RicoSuter.NSwagStudio
```

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