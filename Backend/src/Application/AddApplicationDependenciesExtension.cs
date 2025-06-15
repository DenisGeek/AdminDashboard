using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

/// <summary>
/// Extension class for registering application-level dependencies in the dependency injection container.
/// </summary>
public static class AddApplicationDependenciesExtension
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        //services.AddAutoMapper(typeof(ClientProfile).Assembly);
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetClientsQuery).Assembly));

        return services;
    }
}

