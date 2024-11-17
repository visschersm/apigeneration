namespace Hogwarts.Api.NetContracts;

public record CreateStudent
{
    public required string FirstName { get; init; }
    public required string Surname { get; init; }
}