using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescriptions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Medication)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Dosage)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Frequency)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Duration)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Instructions)
            .HasMaxLength(500);

        builder.HasIndex(p => p.PatientId);
        builder.HasIndex(p => p.DoctorId);
        builder.HasIndex(p => p.MedicalConsultationId);

        builder.HasOne(p => p.Patient)
            .WithMany(pat => pat.Prescriptions)
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.MedicalConsultation)
            .WithMany(mc => mc.Prescriptions)
            .HasForeignKey(p => p.MedicalConsultationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
