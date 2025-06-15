using InfrastructureCommon;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace InfrastructurePostgreSQL;

//public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContextPostgreSQL>
//{
//    public ApplicationDbContextSQLite CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContextSQLite>();
//        optionsBuilder.UseSqlite("Data Source=temp.db");

//        return new ApplicationDbContextSQLite(optionsBuilder.Options);
//    }
//}
