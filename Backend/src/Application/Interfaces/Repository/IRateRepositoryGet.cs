using Domain;

namespace Application;

public interface IRateRepositoryGet
{
    Task<Rate> GetCurrentRateAsync(Currency baseCurrency, Currency targetCurrency);
}
