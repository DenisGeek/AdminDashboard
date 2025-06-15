using Domain;

namespace Application;

public interface IRateRepositoryUpdate
{
    Task UpdateRateAsync(Rate rate);
}
