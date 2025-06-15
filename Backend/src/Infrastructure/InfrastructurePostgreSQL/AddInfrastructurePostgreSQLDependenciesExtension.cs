using InfrastructureCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructurePostgreSQL;

public static class AddInfrastructurePostgreSQLDependenciesExtension
{
    public static IServiceCollection AddInfrastructurePostgreSQLDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InfrastructureCommon.AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

        // Настройка retry policy для production
        services.AddDbContext<InfrastructureCommon.AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("PostgreSQL"),
                o => o.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null)));

        return services;
    }
}

