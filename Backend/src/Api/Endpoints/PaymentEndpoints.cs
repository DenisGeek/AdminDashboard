using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public static class PaymentEndpoints
{
    public static WebApplication MapPaymentEndpoints(this WebApplication app)
    {
        app.MapGet("/payments", 
            async (
                [FromQuery] int? take,
                IMediator mediator,
                CancellationToken ct) =>
        {
            var query = new GetRecentPaymentsQuery(take ?? 5);
            var result = await mediator.Send(query, ct);
            return Results.Ok(result);
        })
        .Produces<IEnumerable<PaymentDto>>(StatusCodes.Status200OK)
        .WithTags("Payments")
        .WithName("GetRecentPayments");

        return app;
    }
}
