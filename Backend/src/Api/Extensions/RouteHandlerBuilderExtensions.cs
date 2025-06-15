using FluentValidation;

namespace Api;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(
        this RouteHandlerBuilder builder)
    {
        return builder.AddEndpointFilter(async (context, next) =>
        {
            var validator = context.HttpContext.RequestServices
                .GetRequiredService<IValidator<TRequest>>();

            var request = context.Arguments
                .OfType<TRequest>()
                .FirstOrDefault();

            if (request is null)
            {
                return Results.BadRequest("Invalid request format");
            }

            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context);
        });
    }
}