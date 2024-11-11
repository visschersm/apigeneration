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

app.MapGet("/students", () =>
{
    return students;
})
.WithName("GetStudents");

app.Run();

record Student(int Id, string FirstName, string Surname)
{
    public string Fullname => $"{FirstName} {Surname}";
}
