using Hogwarts.Api.Domain;
using Hogwarts.Api.NetContracts;
using System.Collections.Generic;

namespace Hogwarts.Api.NetMinimalApi.Mappers;

public static class StudentMapper
{
    public static Domain.Student MapToDomain(NetContracts.CreateStudent student)
    {
        return new Domain.Student
        {
            FirstName = student.FirstName,
            Surname = student.Surname,
        };
    }

    public static NetContracts.DetailedStudent MapToDetailedContract(Domain.Student student)
    {
        return new NetContracts.DetailedStudent
        {
            Id = student.Id,
            FirstName = student.FirstName,
            Surname = student.Surname,
        };
    }

    public static IEnumerable<NetContracts.Student> MapToListContract(IEnumerable<Domain.Student> students)
    {
        return students.Select(student => new NetContracts.Student
        {
            Id = student.Id,
            FirstName = student.FirstName,
            Surname = student.Surname,
        });
    }
}