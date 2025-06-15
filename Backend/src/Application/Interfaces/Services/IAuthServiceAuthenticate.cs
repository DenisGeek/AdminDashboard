namespace Application;

public interface IAuthServiceAuthenticate
{
    Task<AuthResult> AuthenticateAsync(string email, string password);
}
