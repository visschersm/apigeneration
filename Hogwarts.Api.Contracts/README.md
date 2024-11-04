A project using typespec and kiota to create and api

# todo
- Add http test requests
- Add scalar instead of swagger ui
- Try out some complex models
- Figure out how this all would work in ci/cd

# TypeSpec
[Documentation](https://typespec.io/)

```powershell
npm install -g @typespec/compiler
tsp init
tsp install
```
```powershell
tsp compile .\Hogwarts.Api.Contracts\ --output-dir .\Hogwarts.Api.Contracts\tsp-output\
```

# Kiota
[Documentation](https://learn.microsoft.com/en-us/openapi/kiota/overview)

```powershell
dotnet tool install --global Microsoft.OpenApi.Kiota
```
```powershell
kiota generate -l CSharp -n Hogwarts.Api.Client -d '.\Hogwarts.Api.Contracts\tsp-output\@typespec\openapi3\openapi.yaml' -o .\Hogwarts.Api.Client\
```

# NSwag
```powershell
winget install RicoSuter.NSwagStudio
nswag new
```

For now we need to run it from the API folder:
```powershell
dotnet new webapi
nswag run /runtime:net80
```

Otherwise we could do this:
```powershell
nswag run .\Hogwarts.Api\nswag.json /runtime:Net80
```
controller:
```powershell
nswag openapi2cscontroller /input:Hogwarts.Api.Contracts\tsp-output\@typespec\openapi3\openapi.yaml /output:Hogwarts.Api.Contracts
```

# openapi-generator

```powershell
npm install @openapitools/openapi-generator-cli -g
```

```powershell
openapi-generator-cli generate -i '.\Hogwarts.Api.Contracts\tsp-output\@typespec\openapi3\openapi.yaml' -g aspnetcore -o Hogwarts.Api
.\Hogwarts.Api\build.bat

```
```powershell
openapi-generator-cli generate -i '.\Hogwarts.Api.Contracts\tsp-output\@typespec\openapi3\openapi.yaml' -g csharp -o Hogwarts.Api.Client
```