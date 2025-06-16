
using InfrastructureCommon;
using InfrastructureSQLite;
using Microsoft.EntityFrameworkCore;
using Application;

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

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
