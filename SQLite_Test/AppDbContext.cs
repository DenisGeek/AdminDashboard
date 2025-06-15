using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_Test;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Rate> Rates { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=:memory:");
        }
    }

    public static AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        var context = new AppDbContext(options);

        // Открываем соединение и применяем миграции
        context.Database.OpenConnection();
        context.Database.Migrate();
        //context.Database.EnsureCreated(); // Применяет все миграции

        return context;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфигурация Client
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).HasMaxLength(100);
            entity.Property(c => c.Email).HasMaxLength(255);
            entity.Property(c => c.BalanceInTokens).HasColumnType("decimal(18,2)");
            entity.Property(c => c.CreatedAt).IsRequired();
        });

        // Конфигурация Payment
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            entity.Property(p => p.Currency).HasConversion<string>().HasMaxLength(3);
            entity.Property(p => p.Status).HasConversion<string>().HasMaxLength(20);
            entity.Property(p => p.Description).HasMaxLength(500);

            entity.HasOne(p => p.Client)
                  .WithMany(c => c.Payments)
                  .HasForeignKey(p => p.ClientId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Конфигурация Rate
        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Value).HasColumnType("decimal(18,6)");
            entity.Property(r => r.BaseCurrency).HasConversion<string>().HasMaxLength(3);
            entity.Property(r => r.TargetCurrency).HasConversion<string>().HasMaxLength(3);
        });
    }
}
