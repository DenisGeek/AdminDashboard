using Application;
using InfrastructureCommon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureSQLite;

public static class AddInfrastructureCommonDependenciesExtension
{
    public static IServiceCollection AddInfrastructureCommonDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClientsRepositoryGetAll, ClientRepository>();
        services.AddScoped<IPaymentRepositoryGetRecent, PaymentRepository>();
        services.AddScoped<IRateRepositoryGet, RateRepository>();
        services.AddScoped<IRateRepositoryUpdate, RateRepository>();

        services.AddScoped<IAuthServiceAuthenticate, AuthService>();
        services.AddScoped<IUserRepositoryGetByEmail, UserRepository>();
        services.AddScoped<ITokenGenerator, DemoTokenGenerator>(); // По ТЗ возвращает "demo"
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}

