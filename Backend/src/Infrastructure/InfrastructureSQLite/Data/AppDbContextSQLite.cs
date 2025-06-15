using InfrastructureCommon;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureSQLite;

public class AppDbContextSQLite : AppDbContext
{
    public AppDbContextSQLite(DbContextOptions<AppDbContextSQLite> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=:memory:");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
