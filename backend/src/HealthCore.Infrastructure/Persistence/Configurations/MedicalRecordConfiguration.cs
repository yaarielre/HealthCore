using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.ToTable("MedicalRecords");

        builder.HasKey(mr => mr.Id);

        builder.Property(mr => mr.RecordNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(mr => mr.BloodType)
            .HasConversion<int>();

        builder.Property(mr => mr.Allergies)
            .HasMaxLength(1000);

        builder.Property(mr => mr.EmergencyContactName)
            .HasMaxLength(100);

        builder.Property(mr => mr.EmergencyContactPhone)
            .HasMaxLength(20);

        builder.Property(mr => mr.Notes)
            .HasMaxLength(1000);

        builder.Property(mr => mr.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(mr => mr.RecordNumber)
            .IsUnique();

        builder.HasIndex(mr => mr.PatientId)
            .IsUnique();

        builder.HasOne(mr => mr.Patient)
            .WithOne()
            .HasForeignKey<MedicalRecord>(mr => mr.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
