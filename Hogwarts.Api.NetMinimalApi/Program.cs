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

studentsGroup.MapGet("/", async (StudentService service) =>
    await service.GetStudents());

studentsGroup.MapGet("/{id}", async (int id, StudentService service) =>
    await service.GetStuent(id);


app.Run();

record Student(int Id, string FirstName, string Surname)
{
    public string Fullname => $"{FirstName} {Surname}";
}
