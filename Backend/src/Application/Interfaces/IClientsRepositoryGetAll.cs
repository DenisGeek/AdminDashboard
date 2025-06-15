using Domain;

namespace Application;

public interface IClientsRepositoryGetAll
{
    Task<IEnumerable<Client>> GetAllAsync();
}
