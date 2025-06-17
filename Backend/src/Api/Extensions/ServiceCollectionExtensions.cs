using InfrastructureCommon;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Api;

internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures the application to serialize enums as strings in JSON responses.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddEnumAsStringInJsonOutput(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(opts =>
            opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        return services;
    }

    public static IServiceCollection AddSwaggerGenHeader(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AdminDashboard.API",
                Version = "v1"
            });
        });

        return services;
    }
}
