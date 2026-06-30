using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PaymentDate)
            .IsRequired();

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.PaymentMethod)
            .HasConversion<int>()
            .IsRequired()
            .HasSentinel(PaymentMethod.Cash);

        builder.Property(p => p.ReferenceNumber)
            .HasMaxLength(100);

        builder.Property(p => p.Notes)
            .HasMaxLength(500);

        builder.HasIndex(p => p.InvoiceId);
        builder.HasIndex(p => p.PaymentDate);

        builder.HasOne(p => p.Invoice)
            .WithMany(i => i.Payments)
            .HasForeignKey(p => p.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.ReceivedBy)
            .WithMany()
            .HasForeignKey(p => p.ReceivedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
