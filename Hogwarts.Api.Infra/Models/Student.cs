namespace Hogwarts.Api.Infra.Models;

public class Student
{
    public Student(string firstName, string surName)
        => (FirstName, Surname) = (firstName, surName);
    public int Id { get; init; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Fullname => $"{FirstName} {Surname}";
}
