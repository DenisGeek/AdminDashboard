using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthTokenResponseDto>
{
    private readonly IAuthServiceAuthenticate _authService;

    public RefreshTokenCommandHandler(IAuthServiceAuthenticate authService)
    {
        _authService = authService;
    }

    public async Task<AuthTokenResponseDto> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshAsync(request.RefreshToken);

        if (!result.Success)
            throw new UnauthorizedAccessException(result.ErrorMessage);

        return result.Token!;
    }
}
