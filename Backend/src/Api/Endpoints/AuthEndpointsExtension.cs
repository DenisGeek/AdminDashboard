using Application;
using Domain;
//using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public static class AuthEndpointsExtension
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Authentication")
            .WithOpenApi();

        authGroup.MapPost("/login", async (
            [FromBody] LoginRequest request,
            IMediator mediator
            ) =>
        {

            // Обработка
            var command = new LoginCommand(request.Email, request.Password);
            var result = await mediator.Send(command);

            // Успешный ответ
            return TypedResults.Ok(new { result.Token });
        })
        .WithRequestValidation<LoginRequest>()
        .WithName("Login")
        .Produces<AuthResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized);



        return app;
    }
}
