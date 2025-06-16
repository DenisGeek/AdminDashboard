using Domain;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureSQLite.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Fixed GUIDs
            var client1Id = Guid.NewGuid();
            var client2Id = Guid.NewGuid();
            var client3Id = Guid.NewGuid();
            var usdToTokenRateId = Guid.NewGuid();
            var tokentoUsdRateId = Guid.NewGuid();

            // Add clients with 0 initial balance
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name", "Email", "BalanceInTokens", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { client1Id, "Иван Сидоров", "ivan@example.com", 0m, DateTime.Now.AddDays(-10), null },
                    { client2Id, "Ольга Петрова", "olga@example.com", 0m, DateTime.Now.AddDays(-5), null },
                    { client3Id, "Алексей Козлов", "alex@example.com", 0m, DateTime.Now.AddDays(-2), null }
                });

            // Add exchange rates (1 Token = 10 USD, 1 USD = 0.1 Token)
            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Value", "LastUpdated", "BaseCurrency", "TargetCurrency" },
                values: new object[,]
                {
                    {
                        tokentoUsdRateId,
                        10.00m, // 1 Token = 10 USD
                        DateTime.Now,
                        Currency.Token.ToString(),
                        Currency.USD.ToString()
                    },
                    {
                        usdToTokenRateId,
                        0.10m, // 1 USD = 0.1 Token
                        DateTime.Now,
                        Currency.USD.ToString(),
                        Currency.Token.ToString()
                    }
                });

            // Payment data
            var payments = new[]
            {
                new // Payment 1 - Client 1 +100 USD = +10 Tokens (100 * 0.1)
                {
                    Id = Guid.NewGuid(),
                    ClientId = client1Id,
                    Amount = 100.00m,
                    Currency = Currency.USD.ToString(),
                    Status = PaymentStatus.Completed
                },
                new // Payment 2 - Client 2 +50 Tokens
                {
                    Id = Guid.NewGuid(),
                    ClientId = client2Id,
                    Amount = 50.00m,
                    Currency = Currency.Token.ToString(),
                    Status = PaymentStatus.Completed
                },
                new // Payment 3 - Client 3 +200 USD = +20 Tokens (Pending - not counted)
                {
                    Id = Guid.NewGuid(),
                    ClientId = client3Id,
                    Amount = 200.00m,
                    Currency = Currency.USD.ToString(),
                    Status = PaymentStatus.Pending
                },
                new // Payment 4 - Client 1 +75.5 Tokens
                {
                    Id = Guid.NewGuid(),
                    ClientId = client1Id,
                    Amount = 75.50m,
                    Currency = Currency.Token.ToString(),
                    Status = PaymentStatus.Completed
                },
                new // Payment 5 - Client 2 +150 USD = +15 Tokens
                {
                    Id = Guid.NewGuid(),
                    ClientId = client2Id,
                    Amount = 150.00m,
                    Currency = Currency.USD.ToString(),
                    Status = PaymentStatus.Completed
                }
            };

            // Calculate balances
            decimal client1Balance = 0m;
            decimal client2Balance = 0m;
            decimal client3Balance = 0m;

            // Process payments
            foreach (var payment in payments)
            {
                migrationBuilder.InsertData(
                    table: "Payments",
                    columns: new[] { "Id", "ClientId", "Amount", "Currency", "Date", "Description", "Status" },
                    values: new object[]
                    {
                        payment.Id,
                        payment.ClientId,
                        payment.Amount,
                        payment.Currency,
                        DateTime.Now,
                        "Тестовый платеж",
                        payment.Status.ToString()
                    });

                if (payment.Status == PaymentStatus.Completed)
                {
                    decimal amountInTokens = payment.Currency == Currency.USD.ToString()
                        ? payment.Amount * 0.1m // Convert USD to Tokens
                        : payment.Amount;

                    if (payment.ClientId == client1Id)
                        client1Balance += amountInTokens;
                    else if (payment.ClientId == client2Id)
                        client2Balance += amountInTokens;
                    else if (payment.ClientId == client3Id)
                        client3Balance += amountInTokens;
                }
            }

            // Update client balances
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: client1Id,
                columns: new[] { "BalanceInTokens", "UpdatedAt" },
                values: new object[] { client1Balance, DateTime.Now });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: client2Id,
                columns: new[] { "BalanceInTokens", "UpdatedAt" },
                values: new object[] { client2Balance, DateTime.Now });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: client3Id,
                columns: new[] { "BalanceInTokens", "UpdatedAt" },
                values: new object[] { client3Balance, DateTime.Now });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Payments");
            migrationBuilder.Sql("DELETE FROM Rates");
            migrationBuilder.Sql("DELETE FROM Clients");
        }
    }
}
