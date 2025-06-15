using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

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
}
