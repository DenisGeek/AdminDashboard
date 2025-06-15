using Domain;

namespace Application;

public interface ITokenGenerator
{
    string Generate(User user);
}
