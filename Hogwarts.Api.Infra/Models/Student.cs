namespace Hogwarts.Api.Infra.Models;

public class Student
{
    public int Id { get; init; }
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public string Fullname => $"{FirstName} {Surname}";
}
