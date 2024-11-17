using Hogwarts.Api.Infra;
using Hogwarts.Api.Domain;
using Hogwarts.Api.Infra.Models;
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

    public async Task Create(Domain.Student student)
    {
        _context.Students.Add(new Hogwarts.Api.Infra.Models.Student(student.FirstName, student.Surname));
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Student>> Get()
    {
        return await _context.Students.AsNoTracking()
            .Select(x => new Hogwarts.Api.Domain.Student
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Surname = x.Surname
            })
            .ToListAsync();
    }

    public async Task<Domain.Student?> Get(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student is null) return null;
        return new Domain.Student
        {
            Id = student.Id,
            FirstName = student.FirstName,
            Surname = student.Surname
        };
    }

    public async Task Update(int id, Domain.Student inputStudent)
    {
        if (await _context.Students.FindAsync(id) is Hogwarts.Api.Infra.Models.Student studentToUpdate)
        {
            studentToUpdate.FirstName = inputStudent.FirstName;
            studentToUpdate.Surname = inputStudent.Surname;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        if (await _context.Students.FindAsync(id) is Hogwarts.Api.Infra.Models.Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
