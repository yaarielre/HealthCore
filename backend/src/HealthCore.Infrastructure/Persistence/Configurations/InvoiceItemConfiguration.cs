using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("InvoiceItems");

        builder.HasKey(ii => ii.Id);

        builder.Property(ii => ii.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ii => ii.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(ii => ii.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(ii => ii.DiscountPercent)
            .IsRequired()
            .HasColumnType("decimal(5,2)")
            .HasDefaultValue(0m);

        builder.Property(ii => ii.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasIndex(ii => ii.InvoiceId);

        builder.HasOne(ii => ii.Invoice)
            .WithMany(i => i.InvoiceItems)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ii => ii.OrderItem)
            .WithMany()
            .HasForeignKey(ii => ii.OrderItemId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
