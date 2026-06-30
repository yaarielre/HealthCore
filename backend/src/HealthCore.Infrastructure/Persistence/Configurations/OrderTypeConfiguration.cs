using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class OrderTypeConfiguration : IEntityTypeConfiguration<OrderType>
{
    public void Configure(EntityTypeBuilder<OrderType> builder)
    {
        builder.ToTable("OrderTypes");

        builder.HasKey(ot => ot.Id);

        builder.Property(ot => ot.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ot => ot.Description)
            .HasMaxLength(300);

        builder.Property(ot => ot.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
    }
}
