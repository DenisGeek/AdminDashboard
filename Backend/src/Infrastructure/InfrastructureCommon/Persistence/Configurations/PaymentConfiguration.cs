using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCommon;

internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> entity)
    {
        entity.ToTable("Payments");

        entity.HasKey(p => p.Id);
        entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        entity.Property(p => p.Currency).HasConversion<string>().HasMaxLength(3);
        entity.Property(p => p.Status).HasConversion<string>().HasMaxLength(20);
        entity.Property(p => p.Description).HasMaxLength(500);

        entity.HasOne(p => p.Client)
              .WithMany(c => c.Payments)
              .HasForeignKey(p => p.ClientId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}
