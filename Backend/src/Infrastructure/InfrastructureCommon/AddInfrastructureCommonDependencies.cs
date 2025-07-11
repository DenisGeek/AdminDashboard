﻿using Application;
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
        services.AddScoped<IRateRepository, RateRepository>();
        services.AddScoped<IRateRepositoryGet, RateRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddAuthDependencies(configuration);

        return services;
    }
}