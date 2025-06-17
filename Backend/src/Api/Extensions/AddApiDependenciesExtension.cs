using Api;
using FluentValidation;

namespace InfrastructureSQLite;

public static class AddApiDependenciesExtension
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        services.AddScoped<IValidator<UpdateRateRequest>, UpdateRateRequestValidator>();
        
        return services;
    }
}

