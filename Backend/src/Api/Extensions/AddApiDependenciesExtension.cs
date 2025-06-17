using Api;
using FluentValidation;

namespace InfrastructureSQLite;

public static class AddApiDependenciesExtension
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        
        return services;
    }
}

