using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class InsuranceClaimConfiguration : IEntityTypeConfiguration<InsuranceClaim>
{
    public void Configure(EntityTypeBuilder<InsuranceClaim> builder)
    {
        builder.ToTable("InsuranceClaims");

        builder.HasKey(ic => ic.Id);

        builder.Property(ic => ic.InsuranceCompany)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ic => ic.PolicyNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ic => ic.ClaimNumber)
            .HasMaxLength(50);

        builder.Property(ic => ic.ClaimAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(ic => ic.ApprovedAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(ic => ic.Status)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(ClaimStatus.Pending)
            .HasSentinel(ClaimStatus.Pending);

        builder.Property(ic => ic.FiledAt)
            .IsRequired();

        builder.Property(ic => ic.ApprovedAt);

        builder.Property(ic => ic.Notes)
            .HasMaxLength(500);

        builder.HasIndex(ic => ic.InvoiceId);
        builder.HasIndex(ic => ic.PatientId);
        builder.HasIndex(ic => ic.Status);

        builder.HasOne(ic => ic.Invoice)
            .WithMany(i => i.InsuranceClaims)
            .HasForeignKey(ic => ic.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ic => ic.Patient)
            .WithMany()
            .HasForeignKey(ic => ic.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
