using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.ItemName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(oi => oi.Description)
            .HasMaxLength(500);

        builder.Property(oi => oi.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(oi => oi.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oi => oi.Results);

        builder.Property(oi => oi.ResultUrl)
            .HasMaxLength(500);

        builder.Property(oi => oi.ResultedAt);

        builder.Property(oi => oi.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(oi => oi.OrderId);

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oi => oi.ResultedByUser)
            .WithMany()
            .HasForeignKey(oi => oi.ResultedBy)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
