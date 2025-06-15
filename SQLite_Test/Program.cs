using Domain;
using Microsoft.EntityFrameworkCore;

namespace SQLite_Test
{
    public class Program
    {
        public static void Main()
        {
            using (var context = AppDbContext.CreateInMemoryDbContext())
            {
                // Выводим клиентов
                Console.WriteLine("===== КЛИЕНТЫ =====");
                foreach (var client in context.Clients)
                {
                    Console.WriteLine($"ID: {client.Id}");
                    Console.WriteLine($"Имя: {client.Name}");
                    Console.WriteLine($"Email: {client.Email}");
                    Console.WriteLine($"Баланс: {client.BalanceInTokens} токенов");
                    Console.WriteLine($"Дата создания: {client.CreatedAt}");
                    Console.WriteLine($"Дата обновления: {client.UpdatedAt ?? DateTime.MinValue}");
                    Console.WriteLine("------------------");
                }

                // Выводим курсы валют
                Console.WriteLine("\n===== КУРСЫ ВАЛЮТ =====");
                foreach (var rate in context.Rates)
                {
                    Console.WriteLine($"ID: {rate.Id}");
                    Console.WriteLine($"Курс: 1 {rate.BaseCurrency} = {rate.Value} {rate.TargetCurrency}");
                    Console.WriteLine($"Последнее обновление: {rate.LastUpdated}");
                    Console.WriteLine("------------------");
                }

                // Выводим платежи
                Console.WriteLine("\n===== ПЛАТЕЖИ =====");
                foreach (var payment in context.Payments.Include(p => p.Client))
                {
                    Console.WriteLine($"ID: {payment.Id}");
                    Console.WriteLine($"Клиент: {payment.Client!.Name} (ID: {payment.ClientId})");
                    Console.WriteLine($"Сумма: {payment.Amount} {payment.Currency}");
                    Console.WriteLine($"Дата: {payment.Date}");
                    Console.WriteLine($"Статус: {payment.Status}");
                    Console.WriteLine($"Описание: {payment.Description}");
                    Console.WriteLine("------------------");
                }
            }
        }
    }
}
