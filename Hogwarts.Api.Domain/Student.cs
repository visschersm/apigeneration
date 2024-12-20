﻿namespace Hogwarts.Api.Domain;

public record Student
{
    public Student() { }

    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string Surname { get; init; }

    public string Fullname => $"{FirstName} {Surname}";
}
