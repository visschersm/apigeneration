using Hogwarts.Api.Domain;
using Hogwarts.Api.Infra.Models;

namespace Hogwarts.Api.Service.Mappers;

public static class StudentMapper
{
    public static Hogwarts.Api.Infra.Models.Student MapToInfra(Domain.Student student)
    {
        return new Hogwarts.Api.Infra.Models.Student
        {
            FirstName = student.FirstName,
            Surname = student.Surname,
        };
    }
}