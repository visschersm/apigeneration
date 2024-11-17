using Hogwarts.Api.Service;
using Hogwarts.Api.Domain;
using Hogwarts.Api.NetContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var students = new Student[]
{
    new Student(1, "Harry", "Potter"),
    new Student(2, "Ronald", "Weasley"),
    new Student(3, "Hermione", "Granger")
};

var studentsGroup = app.MapGroup("/students");

studentsGroup.MapPost("/", async (Hogwarts.Api.NetContracts.Student student, StudentService service) =>
{
    var domainStudent = new Hogwarts.Api.Domain.Student
    {
        FirstName = student.FirstName,
        Surname = student.Surname
    };
    await service.Create(domainStudent);
});

studentsGroup.MapGet("/", async (StudentService service) =>
{
    var students = await service.Get();
    return students.Select(student => new Hogwarts.Api.NetContracts.Student
    {
        Id = student.Id,
        FirstName = student.FirstName,
        Surname = student.Surname
    });
});


studentsGroup.MapGet("/{id}", async (int id, StudentService service) =>
{
    var student = await service.Get(id);

    if (student is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new Hogwarts.Api.NetContracts.Student
    {
        FirstName = student.FirstName,
        Surname = student.Surname,
    });
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
