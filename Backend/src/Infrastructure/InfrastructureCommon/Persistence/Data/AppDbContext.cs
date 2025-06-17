using Domain;
using InfrastructureCommon.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCommon;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Rate> Rates { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new RateConfiguration());
    }
}