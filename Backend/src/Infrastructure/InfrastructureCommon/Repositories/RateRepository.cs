using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCommon;

internal class RateRepository : IRateRepositoryGet, IRateRepositoryUpdate
{
    private readonly AppDbContext _context;

    public RateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Rate> GetCurrentRateAsync(Currency baseCurrency, Currency targetCurrency)
    {
        return await _context.Rates
            .Where(r => r.BaseCurrency == baseCurrency &&
                       r.TargetCurrency == targetCurrency)
            .OrderByDescending(r => r.LastUpdated)
            .AsNoTracking()
            .FirstAsync();
    }

    public async Task UpdateRateAsync(Rate rate)
    {
        rate.LastUpdated = DateTime.UtcNow;

        var existingRate = await _context.Rates
            .Where(r => r.BaseCurrency == rate.BaseCurrency &&
                       r.TargetCurrency == rate.TargetCurrency)
            .OrderByDescending(r => r.LastUpdated)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (existingRate == null || existingRate.Value != rate.Value)
        {
            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();
        }
    }
}