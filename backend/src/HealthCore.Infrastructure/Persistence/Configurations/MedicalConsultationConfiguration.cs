using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class MedicalConsultationConfiguration : IEntityTypeConfiguration<MedicalConsultation>
{
    public void Configure(EntityTypeBuilder<MedicalConsultation> builder)
    {
        builder.ToTable("MedicalConsultations");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.ReasonForVisit)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.Symptoms)
            .HasMaxLength(2000);

        builder.Property(m => m.Diagnosis)
            .HasMaxLength(2000);

        builder.Property(m => m.Treatment)
            .HasMaxLength(2000);

        builder.Property(m => m.Observations)
            .HasMaxLength(2000);

        builder.Property(m => m.InternalNotes)
            .HasMaxLength(2000);

        builder.HasIndex(m => m.PatientId);
        builder.HasIndex(m => m.DoctorId);
        builder.HasIndex(m => m.AppointmentId);

        builder.HasOne(m => m.Patient)
            .WithMany(p => p.MedicalConsultations)
            .HasForeignKey(m => m.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Doctor)
            .WithMany(d => d.MedicalConsultations)
            .HasForeignKey(m => m.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Appointment)
            .WithOne(a => a.MedicalConsultation)
            .HasForeignKey<MedicalConsultation>(m => m.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
