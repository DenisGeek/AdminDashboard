using Domain;

namespace Application;

public interface IAuthTokenGenerator
{
    AuthTokenResponseDto Generate(User user);
}
