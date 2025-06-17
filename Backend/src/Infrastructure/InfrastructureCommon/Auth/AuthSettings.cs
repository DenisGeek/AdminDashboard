namespace InfrastructureCommon;

/// <summary>
/// Represents authentication settings for the application.
/// </summary>
public class AuthSettings
{
    /// <summary>
    /// The configuration section name for authentication settings.
    /// </summary>
    public const string Position = "AuthSettings";

    /// <summary>
    /// Gets or sets the token issuer.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the token audience.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the secret key for generating access tokens.
    /// </summary>
    public string AccessSecret { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the secret key for generating refresh tokens.
    /// </summary>
    public string RefreshSecret { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expiration time (in minutes) for access tokens.
    /// </summary>
    public int AccessTokenLifetimeMinutes { get; set; } = 15;

    /// <summary>
    /// Gets or sets the expiration time (in days) for refresh tokens.
    /// </summary>
    public int RefreshTokenLifetimeDays { get; set; } = 7;
}
