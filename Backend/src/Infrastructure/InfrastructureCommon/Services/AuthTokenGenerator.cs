using Application;
using Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InfrastructureCommon;

internal class AuthTokenGenerator : IAuthTokenGenerator
{
    private readonly AuthSettings _authSettings;

    public AuthTokenGenerator(
        IOptions<AuthSettings> authSettings)
    {
        _authSettings = authSettings.Value;
    }

    public AuthTokenResponseDto Generate(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var accessKey = Encoding.ASCII.GetBytes(_authSettings.AccessSecret);
        var refreshKey = Encoding.ASCII.GetBytes(_authSettings.RefreshSecret);

        // Create claims for the access token
        var accessClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, AuthConst.Role.Admin)
        };

        // Generate the access token
        var accessTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(accessClaims),
            Expires = DateTime.UtcNow.AddMinutes(_authSettings.AccessTokenLifetimeMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(accessKey),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = _authSettings.Issuer,
            Audience = _authSettings.Audience
        };

        var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);

        // Create claims for the refresh token
        var refreshClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        // Generate the refresh token
        var refreshTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(refreshClaims),
            Expires = DateTime.UtcNow.AddDays(_authSettings.RefreshTokenLifetimeDays),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(refreshKey),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = _authSettings.Issuer,
            Audience = _authSettings.Audience
        };

        var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

        return new AuthTokenResponseDto(
            Access: tokenHandler.WriteToken(accessToken),
            Refresh: tokenHandler.WriteToken(refreshToken)
        );
    }
}