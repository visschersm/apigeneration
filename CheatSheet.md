# TypeSpec
```powershell
npm install -g @typespec/compiler
tsp install
tsp compile .\Hogwarts.Api.TypeSpec.Contracts\ --output-dir .\tsp-output\
```

# NSwag
```powershell
npm install nswag -g
dotnet new webapi -o Hogwarts.Api.NSwagApi -controllers
cd Hogwarts.Api.NSwagApi
nswag new
nswag run .\Hogwarts.Api.NSwagApi\nswag.json
```

# Openapi Generator
```powershell
npm install @openapitools/openapi-generator-cli -g
openapi-generator-cli generate -i '.\Hogwarts.Api.Contracts\openapi.yml' -g aspnetcore -o Hogwarts.Api.OpenAPIGeneratorApi 
.\Hogwarts.Api.OpenAPIGeneratorApi\build.bat
```

# Kiota
```powershell
dotnet tool install --global Microsoft.OpenApi.Kiota
dotnet new classlib -o Hogwarts.Api.KiotaClient
kiota generate -l CSharp -d .\Hogwarts.Api.Contracts\openapi.yml -o .\Hogwarts.Api.KiotaClient\
kiota update -o .\Hogwarts.Api.KiotaClient\
```
