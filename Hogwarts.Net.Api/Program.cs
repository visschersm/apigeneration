using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// MS OpenAPI
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

// NSwag
builder.Services.AddOpenApiDocument();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // MS OpenAPI
    //app.MapOpenApi();

    // NSwag
    app.UseOpenApi(options =>
    {
        options.Path = "/openapi/{documentName}.json";
    });

    // Swagger
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Hogwarts API");
    });

    // Scalar
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

var students = app.MapGroup("/students");
students.MapGet("/", () =>
{
    var students = new Student[]
    {
            new Student(1, "Harry", "Potter")
    };
    return students;
});

students.MapGet("/{id}", (int id) =>
{
    return new Student(1, "Harry", "Potter");
});

app.Run();

record Student(int Id, string FirstName, string LastName)
{
    public string Fullname => $"{FirstName} {LastName}";
}

