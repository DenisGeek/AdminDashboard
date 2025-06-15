using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCommon;

internal class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> entity)
    {
        entity.ToTable("Clients");

        entity.HasKey(c => c.Id);

        entity.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);

        entity.Property(c => c.BalanceInTokens)
            .HasColumnType("decimal(18,2)");

        entity.Property(c => c.CreatedAt)
            .IsRequired();

        entity.Property(c => c.UpdatedAt)
            .IsRequired(false);

        entity.HasIndex(c => c.Email)
            .IsUnique();
    }
}
