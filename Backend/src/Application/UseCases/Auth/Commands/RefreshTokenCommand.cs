using MediatR;

namespace Application;

public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthTokenResponseDto>;
