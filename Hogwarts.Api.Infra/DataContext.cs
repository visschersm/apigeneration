using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Api.Infra;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }

    public DbSet<Models.Student> Students { get; set; }
}