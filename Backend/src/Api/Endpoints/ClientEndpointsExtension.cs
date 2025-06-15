using Application;
using MediatR;

namespace Api;

public static class ClientEndpointsExtension
{
    public static WebApplication MapClientEndpoints(this WebApplication app)
    {
        app.MapGet("/clients",
            async (
                IMediator mediator,
                CancellationToken ct) =>
        {
            var query = new GetClientsQuery();
            var result = await mediator.Send(query, ct);
            return Results.Ok(result);
        })
        .Produces<IEnumerable<ClientDto>>(StatusCodes.Status200OK)
        .WithName("GetAllClients")
        .WithTags("Clients")
        .WithOpenApi();

        return app;
    }
}
