using Domain;

namespace Application;

public interface IRateRepositoryGetAll
{
    Task<IEnumerable<Rate>> GetRateAllAsync();
}
