namespace Application;

/// <summary>
/// Provides authentication services, including user login and token refresh.
/// </summary>
public interface IAuthServiceAuthenticate
{
    /// <summary>
    /// Authenticates a user using the provided username and password.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>
    /// Returns an <see cref="AuthTokenResponseDto"/> containing authentication tokens if authentication is successful;
    /// otherwise, returns null.
    /// </returns>
    Task<AuthResult> AuthenticateAsync(
        string username,
        string password);

    /// <summary>
    /// Refreshes authentication tokens using a valid refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token issued during the initial authentication.</param>
    /// <returns>
    /// Returns an <see cref="AuthTokenResponseDto"/> containing new authentication tokens if the refresh token is valid;
    /// otherwise, returns null.
    /// </returns>
    Task<AuthResult> RefreshAsync(string refreshToken);
}
