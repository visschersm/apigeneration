namespace Hogwarts.Api.NetContracts;

public record DetailedStudent
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public required string Surname { get; init; }
}