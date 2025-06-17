using Application;
using Domain;
using FluentValidation;

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
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            // Обработка
            var command = new LoginCommand(request.Email, request.Password);
            var result = await mediator.Send(command, cancellationToken);

            // Успешный ответ
            return TypedResults.Ok(result);
        })
        .WithRequestValidation<LoginRequest>() // валидация
        .WithName("Login")
        .Produces<AuthTokenResponseDto>(StatusCodes.Status200OK)
        .ProducesValidationProblem();

        authGroup.MapPost("/refresh", async (
            [FromBody] RefreshTokenRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            // Обработка
            var command = new RefreshTokenCommand(request.RefreshToken);
            var result = await mediator.Send(command, cancellationToken);

            // Успешный ответ
            return TypedResults.Ok(result);
        })
        .WithRequestValidation<RefreshTokenRequest>() // валидация
        .WithName("RefreshToken")
        .Produces<AuthTokenResponseDto>(StatusCodes.Status200OK)
        .ProducesValidationProblem();
        return app;
    }
}
