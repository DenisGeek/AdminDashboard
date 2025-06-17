using FluentValidation;

namespace Api;

public sealed class UpdateRateRequestValidator : AbstractValidator<UpdateRateRequest>
{
    public UpdateRateRequestValidator()
    {
        RuleFor(x => x.NewRate)
            .GreaterThan(0)
            .WithMessage("Rate must be greater than zero")
            .LessThan(1000)
            .WithMessage("Rate cannot exceed 1000");
    }
}