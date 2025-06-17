using InfrastructureCommon;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureSQLite;

public static class AddInfrastructureSQLiteDependenciesExtension
{
    public static IServiceCollection AddInfrastructureSQLiteDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Регистрируем соединение как Singleton
        services.AddSingleton<SqliteConnection>(_ => {
            var conn = new SqliteConnection("Data Source=:memory:;Cache=Shared");
            conn.Open();
            return conn;
        });

        // Регистрируем DbContext с использованием общего соединения
        services.AddDbContext<AppDbContext, AppDbContextSQLite>((sp, options) =>
            options.UseSqlite(sp.GetRequiredService<SqliteConnection>()));

        // Регистрируем hosted service для применения миграций
        services.AddHostedService<SqliteMigrationHostedService>();
        
        return services;
    }
}

