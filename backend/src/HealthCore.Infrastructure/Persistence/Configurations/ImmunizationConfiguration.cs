using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class ImmunizationConfiguration : IEntityTypeConfiguration<Immunization>
{
    public void Configure(EntityTypeBuilder<Immunization> builder)
    {
        builder.ToTable("Immunizations");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.VaccineName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.DoseNumber)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(i => i.ApplicationDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(i => i.NextDoseDate)
            .HasColumnType("date");

        builder.Property(i => i.BatchNumber)
            .HasMaxLength(50);

        builder.Property(i => i.AdministeredBy)
            .HasMaxLength(100);

        builder.Property(i => i.Notes)
            .HasMaxLength(500);

        builder.HasIndex(i => i.PatientId);

        builder.HasOne(i => i.Patient)
            .WithMany()
            .HasForeignKey(i => i.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
