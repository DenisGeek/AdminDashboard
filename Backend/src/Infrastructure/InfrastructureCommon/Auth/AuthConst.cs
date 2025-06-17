using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InfrastructureCommon;

/// <summary>
/// Provides role constants used for authorization throughout the application.
/// </summary>
public static class AuthConst
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string ClientId = "ClientId";
    }

    public static class Jwt
    {
        public const string Scheme = JwtBearerDefaults.AuthenticationScheme;
        public const string Bearer = "Bearer";
        public const string AuthorizationHeader = "Authorization";
    }

    public static class Policies
    {
        public const string AdminOnly = "AdminOnly";
        public const string UserOnly = "UserOnly";
    }

    public static class Swagger
    {
        public const string SecuritySchemeDescription =
            "JWT Authorization header using the Bearer scheme.\n\n" +
            "Enter 'Bearer' [space] and then your token.\n\n" +
            "Example: 'Bearer 12345abcdef'";
    }

    public static class ErrorMessages
    {
        public const string TokenExpired = "Token validation failed - token expired";
        public const string TokenInvalidSignature = "Token validation failed - invalid signature";
        public const string InvalidCredentials = "Invalid email or password";
        public const string AuthenticationFailed = "Authentication failed";
        public const string InvalidRefreshToken = "Invalid refresh token";
        public const string InvalidTokenContent = "Invalid token content";
        public const string UserNotFound = "User not found";
        public const string TokenRefreshFailed = "Token refresh failed";
        public const string MissingOrInvalidConfigurationSection = "Configuration section '{0}' is missing or invalid";
    }

    public static class LogMessages
    {
        // Аутентификация
        public const string AuthenticationFailedUserNotFound = "Authentication failed for email: {Email} - user not found";
        public const string AuthenticationFailedInvalidPassword = "Authentication failed for user {UserId} - invalid password";
        public const string AuthenticationSuccessful = "User {UserId} authenticated successfully";
        public const string AuthenticationError = "Error during authentication for email: {Email}";

        // Обновление токена (Refresh)
        public const string RefreshFailedInvalidToken = "Refresh failed - invalid refresh token";
        public const string RefreshFailedInvalidUserId = "Refresh failed - missing or invalid user ID in token";
        public const string RefreshFailedUserNotFound = "Refresh failed - user {UserId} not found";
        public const string RefreshSuccessful = "Token refreshed successfully for user {UserId}";
        public const string RefreshError = "Error during token refresh";

        // Валидация токена
        public const string TokenValidationExpired = "Token validation failed - token expired";
        public const string TokenValidationInvalidSignature = "Token validation failed - invalid signature";
        public const string TokenValidationError = "Token validation failed";
    }
}
