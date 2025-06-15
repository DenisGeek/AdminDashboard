using MediatR;

namespace Application;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IAuthServiceAuthenticate _authService;

    public LoginCommandHandler(IAuthServiceAuthenticate authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken ct)
    {
        var result = await _authService.AuthenticateAsync(request.Email, request.Password);

        if (!result.Success)
            throw new UnauthorizedAccessException(result.ErrorMessage);

        return new AuthResponse(result.Token!);
    }
}
