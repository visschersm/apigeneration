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
tsp compile .\Hogwarts.Api.TypeSpec.Contracts\ --output-dir .\tsp-output\
```

### Update contracts
```powershell
tsp compile .\Hogwarts.Api.TypeSpec.Contracts\ --output-dir .\tsp-output\
```

### Genearting TypeSpec from an OpenAPI specification
Running the following command will generate a `main.tsp` file from a OpenAPI specification. This can be used as a starting point for developing your specification using TypeSpec.

```powershell
tsp-openapi3 '.\tsp-output\@typespec\openapi3\openapi.yaml' --output-dir 'tsp-output\generated-typespec-project\'
```

You will still need to add an config and package file. It helps running the tsp setup commands first: 
```powershell
tsp init
tsp install
```

This will initialize your TypeSpec project as normal and if you run the `tsp-openapi3` command it will override your `main.tsp` file and generate your starting point of setting up your openapi specification using TypeSpec. It is noted on their website that this tool is supposed to be used only once. To generate the starting point.
