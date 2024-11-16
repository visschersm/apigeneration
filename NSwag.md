# Install NSwag
```powershell
npm install nswag -g
```

# Setting up new Project
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

# Create a client
## NSwag
```powershell
mkdir Hogwarts.Api.NSwagClient
cd Hogwarts.Api.NSwagClient
nswag new
```

Change the `documentGenerator/fromdocument/url` to `"../Hogwarts.Api.Contracts/openapi.yml"` so that the generator can find our openapi specification.

From root:
```powershell
nswag run .\Hogwarts.Api.NSwagClient\nswag.client.json
```

# Generating API from the same NSwag json
We could also decide we want to generate both the client and the server from the same nswag file.
This is also possible and pretty straight forward. We just run the `nswag new` command in the folder that we want the nswag definition file in.

```powershell
nswag new
```

Now we can just put both the client and the server generators in this file:
```json
"documentGenerator": {
    "fromDocument": {
      "url": "../Hogwarts.Api.Contracts/openapi.yml",
      ...
    }
  },
"codeGenerators": {
    "openApiToCSharpClient": {
        ...
        "output": "../Hogwarts.Api.NSwagClient/Hogwarts.Api.NSwagClient.g.cs",
    },
    "openApiToCSharpController": {
        ...
        "output": "../Hogwarts.Api.NSwagApi/Controllers/Hogwarts.Api.NSwagApi.g.cs",
    }
}
```

When running the command:
```powershell
nswag run
```
it will detect the settings defined in `nswag.json` and generate both the client and server.


# TODO:
- Create sample on how to use this in a working sample.