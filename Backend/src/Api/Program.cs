using Application;
using InfrastructureSQLite;
using Microsoft.OpenApi.Models;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddInfrastructureCommonDependencies(builder.Configuration)
            .AddInfrastructureSQLiteDependencies(builder.Configuration)
            .AddApplicationDependencies(builder.Configuration)
            .AddApiDependencies(builder.Configuration);

        builder.Services
            .AddAuthorization()
            .AddEndpointsApiExplorer()
            .AddSwaggerGenHeader();

        builder.Services.AddEnumAsStringInJsonOutput();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        app.UseCors("AllowAll");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app
            .MapAuthEndpoints()
            .MapClientEndpoints()
            .MapPaymentEndpoints()
            .MapRateEndpoints();


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}
