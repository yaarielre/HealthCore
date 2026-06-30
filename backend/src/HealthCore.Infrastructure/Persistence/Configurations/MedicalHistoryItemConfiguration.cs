using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class MedicalHistoryItemConfiguration : IEntityTypeConfiguration<MedicalHistoryItem>
{
    public void Configure(EntityTypeBuilder<MedicalHistoryItem> builder)
    {
        builder.ToTable("MedicalHistoryItems");

        builder.HasKey(mhi => mhi.Id);

        builder.Property(mhi => mhi.Category)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(mhi => mhi.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(mhi => mhi.Details)
            .HasMaxLength(2000);

        builder.Property(mhi => mhi.RecordedDate);

        builder.Property(mhi => mhi.Severity);

        builder.Property(mhi => mhi.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(mhi => mhi.PatientId);
        builder.HasIndex(mhi => mhi.Category);

        builder.HasOne(mhi => mhi.Patient)
            .WithMany()
            .HasForeignKey(mhi => mhi.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mhi => mhi.RecordedBy)
            .WithMany()
            .HasForeignKey(mhi => mhi.RecordedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
