using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.IdNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(p => p.Email)
            .HasMaxLength(150);

        builder.Property(p => p.BirthDate)
            .IsRequired();

        builder.Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Gender)
            .HasConversion<int>();

        builder.Property(p => p.BloodType)
            .HasConversion<int>();

        builder.Property(p => p.Allergies)
            .HasMaxLength(1000);

        builder.Property(p => p.EmergencyContactName)
            .HasMaxLength(100);

        builder.Property(p => p.EmergencyContactPhone)
            .HasMaxLength(20);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(p => p.IdNumber)
            .IsUnique();

        builder.HasIndex(p => p.LastName);
        builder.HasIndex(p => p.Phone);

        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.MedicalConsultations)
            .WithOne(m => m.Patient)
            .HasForeignKey(m => m.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Prescriptions)
            .WithOne(p => p.Patient)
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
