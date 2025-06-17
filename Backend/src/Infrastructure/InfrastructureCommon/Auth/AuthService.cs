using Application;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InfrastructureCommon;

internal class AuthService : IAuthServiceAuthenticate
{
    private readonly ILogger<AuthService> _logger;
    private readonly AuthSettings _authSettings;
    private readonly IUserRepository _userRepository;
    private readonly IAuthPasswordHasher _passwordHasher;
    private readonly IAuthTokenGenerator _tokenGenerator;

    public AuthService(
        ILogger<AuthService> logger,
        IOptions<AuthSettings> authSettings,
        IUserRepository userRepository,
        IAuthPasswordHasher passwordHasher,
        IAuthTokenGenerator tokenGenerator)
    {
        _logger = logger;
        _authSettings = authSettings.Value;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResult> AuthenticateAsync(string email, string password)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning(AuthConst.LogMessages.AuthenticationFailedUserNotFound, email);
                return AuthResult.Failed(AuthConst.ErrorMessages.InvalidCredentials);
            }

            if (!_passwordHasher.Verify(password, user.PasswordHash))
            {
                _logger.LogWarning(AuthConst.LogMessages.AuthenticationFailedInvalidPassword, user.Id);
                return AuthResult.Failed(AuthConst.ErrorMessages.InvalidCredentials);
            }

            var token = _tokenGenerator.Generate(user);
            _logger.LogInformation(AuthConst.LogMessages.AuthenticationSuccessful, user.Id);
            return AuthResult.Successful(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, AuthConst.LogMessages.AuthenticationError, email);
            return AuthResult.Failed(AuthConst.ErrorMessages.AuthenticationFailed);
        }
    }

    public async Task<AuthResult> RefreshAsync(string refreshToken)
    {
        try
        {
            var principal = GetPrincipalFromToken(refreshToken, isAccessToken: false);
            if (principal == null)
            {
                _logger.LogWarning(AuthConst.LogMessages.RefreshFailedInvalidToken);
                return AuthResult.Failed(AuthConst.ErrorMessages.InvalidRefreshToken);
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                _logger.LogWarning(AuthConst.LogMessages.RefreshFailedInvalidUserId);
                return AuthResult.Failed(AuthConst.ErrorMessages.InvalidTokenContent);
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning(AuthConst.LogMessages.RefreshFailedUserNotFound, userId);
                return AuthResult.Failed(AuthConst.ErrorMessages.UserNotFound);
            }

            var newToken = _tokenGenerator.Generate(user);
            _logger.LogInformation(AuthConst.LogMessages.RefreshSuccessful, userId);
            return AuthResult.Successful(newToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, AuthConst.LogMessages.RefreshError);
            return AuthResult.Failed(AuthConst.ErrorMessages.TokenRefreshFailed);
        }
    }

    protected virtual ClaimsPrincipal? GetPrincipalFromToken(string token, bool isAccessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = isAccessToken
            ? Encoding.ASCII.GetBytes(_authSettings.AccessSecret)
            : Encoding.ASCII.GetBytes(_authSettings.RefreshSecret);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _authSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _authSettings.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero // Точная проверка времени без допуска
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch (SecurityTokenExpiredException)
        {
            _logger.LogWarning(AuthConst.LogMessages.TokenValidationExpired);
            return null;
        }
        catch (SecurityTokenInvalidSignatureException)
        {
            _logger.LogWarning(AuthConst.LogMessages.TokenValidationInvalidSignature);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, AuthConst.LogMessages.TokenValidationError);
            return null;
        }
    }
}