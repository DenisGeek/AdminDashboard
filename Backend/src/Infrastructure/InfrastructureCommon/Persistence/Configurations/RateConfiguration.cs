using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureCommon.Configurations;

internal class RateConfiguration : IEntityTypeConfiguration<Rate>
{
    public void Configure(EntityTypeBuilder<Rate> entity)
    {
        entity.ToTable("Rates");

        entity.HasKey(r => r.Id);
        entity.Property(r => r.Value).HasColumnType("decimal(18,6)");
        entity.Property(r => r.BaseCurrency).HasConversion<string>().HasMaxLength(5);
        entity.Property(r => r.TargetCurrency).HasConversion<string>().HasMaxLength(5);
    }
}