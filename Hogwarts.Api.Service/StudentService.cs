using Hogwarts.Api.Infra;
using Hogwarts.Api.Domain;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Api.Service;

public class StudentService
{
    private readonly DataContext _context;

    public StudentService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Student>> GetStudents()
    {
        return await _context.Students.AsNoTracking()
            .Select(x => new Domain.Student(x.Id, x.FirstName, x.Surname))
            .ToListAsync();
    }

    public async Task<Domain.Student?> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student is null) return null;
        return new Domain.Student(student.Id, student.FirstName, student.Surname);
    }
}
