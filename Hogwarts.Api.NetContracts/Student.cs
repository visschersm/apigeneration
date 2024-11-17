namespace Hogwarts.Api.NetContracts;

public record Student
{
    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string Surname { get; init; }
    public string Fullname => $"{FirstName} {Surname}";
}
