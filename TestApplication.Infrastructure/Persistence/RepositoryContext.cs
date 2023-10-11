using Microsoft.EntityFrameworkCore;
using TestApplication.Core;

namespace TestApplication.Infrastructure.Persistence;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Test> Tests { get; set; }
}