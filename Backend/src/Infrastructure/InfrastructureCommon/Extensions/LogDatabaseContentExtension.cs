using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InfrastructureCommon;

public static class LogDatabaseContentExtension
{
    private static void LogDatabaseContent(this AppDbContext dbContext, ILogger logger)
    {
        try
        {
            logger.LogInformation("===== DATABASE CONTENT =====");

            // Клиенты
            logger.LogInformation("--- CLIENTS ---");
            foreach (var client in dbContext.Clients.AsNoTracking())
            {
                logger.LogInformation($"ID: {client.Id}, Name: {client.Name}, Email: {client.Email}, Balance: {client.BalanceInTokens}");
            }

            // Платежи
            logger.LogInformation("\n--- PAYMENTS ---");
            foreach (var payment in dbContext.Payments.AsNoTracking().Include(p => p.Client))
            {
                logger.LogInformation($"ID: {payment.Id}, Client: {payment.Client!.Name}, Amount: {payment.Amount} {payment.Currency}, Status: {payment.Status}");
            }

            // Курсы
            logger.LogInformation("\n--- RATES ---");
            foreach (var rate in dbContext.Rates.AsNoTracking())
            {
                logger.LogInformation($"ID: {rate.Id}, Rate: {rate.BaseCurrency}->{rate.TargetCurrency} = {rate.Value}, Updated: {rate.LastUpdated}");
            }

            logger.LogInformation("===== END DATABASE CONTENT =====");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while logging database content");
        }
    }
}
