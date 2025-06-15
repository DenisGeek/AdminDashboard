using InfrastructureCommon;
using Microsoft.EntityFrameworkCore;

namespace InfrastructurePostgreSQL;

public class ApplicationDbContextPostgreSQL : AppDbContext
{
    public ApplicationDbContextPostgreSQL(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
