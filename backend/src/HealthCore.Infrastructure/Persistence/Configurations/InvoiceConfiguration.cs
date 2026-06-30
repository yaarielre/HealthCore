using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.InvoiceNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(i => i.InvoiceDate)
            .IsRequired();

        builder.Property(i => i.DueDate)
            .IsRequired();

        builder.Property(i => i.SubTotal)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m);

        builder.Property(i => i.TaxAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m);

        builder.Property(i => i.DiscountAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m);

        builder.Property(i => i.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m);

        builder.Property(i => i.PaidAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m);

        builder.Property(i => i.BalanceDue)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0m);

        builder.Property(i => i.Status)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(InvoiceStatus.Unpaid)
            .HasSentinel(InvoiceStatus.Unpaid);

        builder.Property(i => i.Notes)
            .HasMaxLength(500);

        builder.Property(i => i.CancelledAt);

        builder.HasIndex(i => i.InvoiceNumber)
            .IsUnique();

        builder.HasIndex(i => i.PatientId);
        builder.HasIndex(i => i.Status);
        builder.HasIndex(i => i.InvoiceDate);

        builder.HasOne(i => i.Patient)
            .WithMany()
            .HasForeignKey(i => i.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.IssuedBy)
            .WithMany()
            .HasForeignKey(i => i.IssuedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
