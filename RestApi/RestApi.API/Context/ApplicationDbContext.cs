using Microsoft.EntityFrameworkCore;
using RestApi.API.Model;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
}