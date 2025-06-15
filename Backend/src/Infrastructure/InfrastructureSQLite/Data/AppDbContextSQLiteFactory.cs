using InfrastructureCommon;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureSQLite;

public class AppDbContextSQLiteFactory : IDesignTimeDbContextFactory<AppDbContextSQLite>
{
    public AppDbContextSQLite CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContextSQLite>();
        optionsBuilder.UseSqlite("Data Source=:memory:");

        return new AppDbContextSQLite(optionsBuilder.Options);
    }
}
