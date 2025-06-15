using Domain;

namespace Application;

public interface IPaymentRepositoryGetRecent
{
    Task<IEnumerable<Payment>> GetRecentPaymentsAsync(int take);
    Task<int> GetTotalPaymentsCountAsync();
}
