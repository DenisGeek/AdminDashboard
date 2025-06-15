using Application;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public static class RateEndpoints
{
    public static WebApplication MapRateEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/rate")
            .WithTags("Rate");

        group.MapGet("/", 
            async (
                [FromQuery] string? baseCurrency,
                [FromQuery] string? targetCurrency,
                IMediator mediator) =>
        {
            var baseCurr = Enum.TryParse<Currency>(baseCurrency, out var b)
                ? b : Currency.USD;
            var targetCurr = Enum.TryParse<Currency>(targetCurrency, out var t)
                ? t : Currency.Token;

            var query = new GetCurrentRateQuery(baseCurr, targetCurr);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .Produces<RateDto>(StatusCodes.Status200OK)
        .WithName("GetCurrentRate");

        group.MapPost("/", 
            async (
                [FromBody] UpdateRateRequest request,
                IMediator mediator) =>
        {
            var baseCurr = Enum.TryParse<Currency>(request.BaseCurrency, out var b)
                ? b : Currency.USD;
            var targetCurr = Enum.TryParse<Currency>(request.TargetCurrency, out var t)
                ? t : Currency.Token;

            var command = new UpdateRateCommand(
                request.NewRate,
                baseCurr,
                targetCurr);

            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .Produces<RateDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithName("UpdateRate");

        return app;
    }
}