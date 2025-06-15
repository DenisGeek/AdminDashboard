using Domain;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureSQLite.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Фиксированные GUID для последующего использования
            var client1Id = Guid.NewGuid();
            var client2Id = Guid.NewGuid();
            var rate1Id = Guid.NewGuid();
            var rate2Id = Guid.NewGuid();
            var rate3Id = Guid.NewGuid();

            // Добавляем клиентов
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name", "Email", "BalanceInTokens", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    {
                        client1Id,
                        "Алексей Иванов",
                        "alex@example.com",
                        1500.00m,
                        new DateTime(2023, 1, 1),
                        null
                    },
                    {
                        client2Id,
                        "Елена Петрова",
                        "elena@example.com",
                        750.50m,
                        new DateTime(2023, 1, 2),
                        null
                    }
                });

            // Добавляем курсы валют
            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Value", "LastUpdated", "BaseCurrency", "TargetCurrency" },
                values: new object[,]
                {
                    {
                        rate1Id,
                        1.00m, // 1 Token = 1 Token
                        new DateTime(2023, 1, 1),
                        Currency.Token.ToString(),
                        Currency.Token.ToString()
                    },
                    {
                        rate2Id,
                        0.01m, // 1 Token = 0.1 USD
                        new DateTime(2023, 1, 1),
                        Currency.Token.ToString(),
                        Currency.USD.ToString()
                    },
                    {
                        rate3Id,
                        9.00m, // 1 USD = 0.9 Token
                        new DateTime(2023, 1, 1),
                        Currency.USD.ToString(),
                        Currency.Token.ToString()
                    }
                });

            // Добавляем платежи
            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "ClientId", "Amount", "Currency", "Date", "Description", "Status" },
                values: new object[,]
                {
                    {
                        Guid.NewGuid(),
                        client1Id,
                        100.00m,
                        Currency.Token.ToString(),
                        new DateTime(2023, 1, 10),
                        "Пополнение счета",
                        PaymentStatus.Completed.ToString()
                    },
                    {
                        Guid.NewGuid(),
                        client2Id,
                        50.00m,
                        Currency.USD.ToString(),
                        new DateTime(2023, 1, 11),
                        "Оплата услуги",
                        PaymentStatus.Pending.ToString()
                    },
                    {
                        Guid.NewGuid(),
                        client1Id,
                        200.00m,
                        Currency.RUB.ToString(),
                        new DateTime(2023, 1, 12),
                        "Возврат средств",
                        PaymentStatus.Failed.ToString()
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаляем все добавленные данные в обратном порядке
            migrationBuilder.Sql("DELETE FROM Payments");
            migrationBuilder.Sql("DELETE FROM Rates");
            migrationBuilder.Sql("DELETE FROM Clients");
        }
    }
}
