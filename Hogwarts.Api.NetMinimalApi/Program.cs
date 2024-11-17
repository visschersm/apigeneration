using Hogwarts.Api.Service;
using Hogwarts.Api.Domain;
using Hogwarts.Api.NetContracts;
using Hogwarts.Api.Infra;
using Microsoft.EntityFrameworkCore;
using Hogwarts.Api.NetMinimalApi.Mappers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Hogwarts"));

// NSwag
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "HogwartsApi";
    config.Title = "HogwartsApi v1";
    config.Version = "v1";
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddTransient<StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "HogwartsAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseHttpsRedirection();

var studentsGroup = app.MapGroup("/students");

studentsGroup.MapPost("/", async (Hogwarts.Api.NetContracts.CreateStudent student, StudentService service) =>
{
    var domainStudent = StudentMapper.MapToDomain(student);
    await service.Create(domainStudent);
});

studentsGroup.MapGet("/", async (StudentService service) =>
{
    var students = await service.Get();
    var studentList = StudentMapper.MapToListContract(students);
    return studentList;
});


studentsGroup.MapGet("/{id}", async (int id, StudentService service) =>
{
    var student = await service.Get(id);

    if (student is null)
    {
        return Results.NotFound();
    }

    var detailedStudent = StudentMapper.MapToDetailedContract(student);

    return Results.Ok(detailedStudent);
});

studentsGroup.MapPut("/{id}", async (int id, Hogwarts.Api.NetContracts.Student inputStudent, StudentService service) =>
{
    var student = new Hogwarts.Api.Domain.Student
    {
        FirstName = inputStudent.FirstName,
        Surname = inputStudent.Surname,
    };
    await service.Update(inputStudent.Id, student);
});

studentsGroup.MapDelete("/{id}", async (int id, StudentService service) =>
{
    await service.Delete(id);
});

app.Run();

record Student(int Id, string FirstName, string Surname)
{
    public string Fullname => $"{FirstName} {Surname}";
}
