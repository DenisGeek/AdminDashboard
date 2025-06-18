using Application;
using Domain;
using InfrastructureCommon;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public static class RateEndpoints
{
    public static WebApplication MapRateEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/rate")
            .RequireAuthorization()
            .WithTags("Rate");

        // GetCurrentRate
        group.MapGet("/", 
            async (
                [FromQuery] string? baseCurrency,
                [FromQuery] string? targetCurrency,
                IMediator mediator) =>
        {
            var baseCurr = Enum.TryParse<Currency>(baseCurrency, out var b)
                ? b : Currency.Token;
            var targetCurr = Enum.TryParse<Currency>(targetCurrency, out var t)
                ? t : Currency.USD;

            var query = new GetCurrentRateQuery(baseCurr, targetCurr);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .Produces<RateDto>(StatusCodes.Status200OK)
        .WithName("GetCurrentRate");

        // UpdateRate
        group.MapPost("/", 
            async (
                [FromBody] UpdateRateRequest request,
                IMediator mediator) =>
        {
            var baseCurr = Enum.TryParse<Currency>(request.BaseCurrency, out var b)
                ? b : Currency.Token;
            var targetCurr = Enum.TryParse<Currency>(request.TargetCurrency, out var t)
                ? t : Currency.USD;

            var command = new UpdateRateCommand(
                request.NewRate,
                baseCurr,
                targetCurr);

            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .RequireAuthorization(policy => policy.RequireRole(AuthConst.Role.Admin))
        .WithRequestValidation<UpdateRateRequest>() // валидация
        .Produces<RateDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("UpdateRate");


        // GetAllRates
        group.MapGet("/all", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllRatesQuery());
            return Results.Ok(result);
        })
        .WithName("GetAllRates")
        .Produces<IEnumerable<Rate>>(StatusCodes.Status200OK);

        return app;
    }
}