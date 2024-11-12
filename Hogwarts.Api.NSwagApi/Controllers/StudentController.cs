namespace Hogwarts.Api.NSwagApi;

public class StudentController : IController
{
    public Task<ICollection<Student>> GetStudentsAsync()
    {
        return Task.FromResult<ICollection<Student>>(new Student[]
        {
            new Student
            {
                Id = 1,
                FirstName = "Harry",
                Surname ="Potter"
            },
            new Student
            {
                Id = 2,
                FirstName = "Ronald",
                Surname = "Weasley"
            },
            new Student
            {
                Id = 3,
                FirstName = "Hermione",
                Surname = "Granger"
            }
        });
    }
}