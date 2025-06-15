using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCommon;

internal class PaymentRepository: IPaymentRepositoryGetRecent
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payment>> GetRecentPaymentsAsync(int take)
    {
        return await _context.Payments
            .OrderByDescending(p => p.Date)
            .Take(take)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> GetTotalPaymentsCountAsync()
    {
        return await _context.Payments.CountAsync();
    }
}
