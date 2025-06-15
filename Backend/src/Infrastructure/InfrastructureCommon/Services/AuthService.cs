using Application;

namespace InfrastructureCommon;

internal class AuthService : IAuthServiceAuthenticate
{
    private readonly IUserRepositoryGetByEmail _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthService(
        IUserRepositoryGetByEmail userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResult> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return AuthResult.Failed("Invalid email or password");

        if (!_passwordHasher.Verify(password, user.PasswordHash))
            return AuthResult.Failed("Invalid email or password");

        var token = _tokenGenerator.Generate(user);
        return AuthResult.Successful(token);
    }
}