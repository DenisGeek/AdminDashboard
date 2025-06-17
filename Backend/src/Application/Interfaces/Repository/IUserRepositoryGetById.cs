using Domain;

namespace Application;

public interface IUserRepositoryGetById
{
    Task<User?> GetByIdAsync(Guid id);
}
