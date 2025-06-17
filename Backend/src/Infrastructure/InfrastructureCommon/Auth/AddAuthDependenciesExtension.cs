using Application;
using FluentValidation;
using InfrastructureCommon;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace InfrastructureSQLite;

public static class AddAuthDependenciesExtension
{
    public static IServiceCollection AddAuthDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var authSettings = services.ValidateAndRegisterAuthSettings(configuration);
        services.AddJwtAuthentication(authSettings);
        services.AddSwaggerGenJWT();
        services.AddValidatorsFromAssemblyContaining<AuthSettingsValidator>();
        services.AddScoped<IAuthTokenGenerator, AuthTokenGenerator>();
        services.AddScoped<IAuthPasswordHasher, AuthBCryptPasswordHasher>();
        services.AddScoped<IAuthServiceAuthenticate, AuthService>();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AuthSettings authSettings)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = AuthConst.Jwt.Scheme;
            options.DefaultChallengeScheme = AuthConst.Jwt.Scheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authSettings.Issuer,
                ValidAudience = authSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(authSettings.AccessSecret)),
                ValidAlgorithms = [SecurityAlgorithms.HmacSha256]
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthConst.Policies.AdminOnly, policy => policy.RequireRole(AuthConst.Role.Admin));
            options.AddPolicy(AuthConst.Policies.UserOnly, policy => policy.RequireRole(AuthConst.Role.User));
        });

        return services;
    }

    public static IServiceCollection AddSwaggerGenJWT(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition(AuthConst.Jwt.Bearer, new OpenApiSecurityScheme
            {
                Description = AuthConst.Swagger.SecuritySchemeDescription,
                Name = AuthConst.Jwt.AuthorizationHeader,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = AuthConst.Jwt.Bearer
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = AuthConst.Jwt.Bearer
                    },
                    Name = AuthConst.Jwt.Bearer,
                    In = ParameterLocation.Header
                },
                Array.Empty<string>()
            }
        });
        });

        return services;
    }

    private static AuthSettings ValidateAndRegisterAuthSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authSettingsSection = configuration.GetSection(AuthSettings.Position);
        var authSettings = authSettingsSection.Get<AuthSettings>();

        if (authSettings == null)
        {
            throw new InvalidOperationException(
                string.Format(AuthConst.ErrorMessages.MissingOrInvalidConfigurationSection, AuthSettings.Position));
        }

        var validator = new AuthSettingsValidator();
        validator.ValidateAndThrow(authSettings);

        services.Configure<AuthSettings>(authSettingsSection);
        services.AddSingleton(authSettings);

        return authSettings;
    }
}