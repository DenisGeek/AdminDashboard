using FluentValidation;

namespace InfrastructureCommon;

public class AuthSettingsValidator : AbstractValidator<AuthSettings>
{
    public AuthSettingsValidator()
    {
        RuleFor(x => x.Issuer)
            .NotEmpty().WithMessage("Issuer is required")
            .Must(BeValidUri).WithMessage("Issuer must be a valid URI (e.g., 'https://api.example.com')")
            .Length(5, 100).WithMessage("Issuer must be between 5 and 100 characters");

        RuleFor(x => x.Audience)
            .NotEmpty().WithMessage("Audience is required")
            .Must(BeValidUri).WithMessage("Audience must be a valid URI (e.g., 'https://app.example.com')")
            .Length(5, 100).WithMessage("Audience must be between 5 and 100 characters");

        RuleFor(x => x.AccessSecret)
            .NotEmpty().WithMessage("Access secret is required")
            .MinimumLength(32).WithMessage("Access secret must be at least 32 characters")
            .MaximumLength(512).WithMessage("Access secret must not exceed 512 characters");

        RuleFor(x => x.RefreshSecret)
            .NotEmpty().WithMessage("Refresh secret is required")
            .MinimumLength(32).WithMessage("Refresh secret must be at least 32 characters")
            .MaximumLength(512).WithMessage("Refresh secret must not exceed 512 characters")
            .NotEqual(x => x.AccessSecret).WithMessage("Refresh secret must be different from access secret");

        RuleFor(x => x.AccessTokenLifetimeMinutes)
            .GreaterThan(0).WithMessage("Access token lifetime must be positive")
            .LessThanOrEqualTo(1440).WithMessage("Access token lifetime cannot exceed 24 hours (1440 minutes)");

        RuleFor(x => x.RefreshTokenLifetimeDays)
            .GreaterThan(0).WithMessage("Refresh token lifetime must be positive")
            .LessThanOrEqualTo(365).WithMessage("Refresh token lifetime cannot exceed 1 year");
    }

    private bool BeValidUri(string uri)
    {
        return Uri.TryCreate(uri, UriKind.Absolute, out _);
    }
}
