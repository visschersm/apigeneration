namespace Hogwarts.Api.Infra.Models;

public record Student(int Id, string FirstName, string Surname)
{
    public string Fullname => $"{FirstName} {Surname}";
}
