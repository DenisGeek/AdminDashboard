using Domain;

namespace Application;

public interface IUserRepositoryGetByEmail
{
    Task<User?> GetByEmailAsync(string email);
}
