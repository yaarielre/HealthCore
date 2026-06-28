using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class VitalSignConfiguration : IEntityTypeConfiguration<VitalSign>
{
    public void Configure(EntityTypeBuilder<VitalSign> builder)
    {
        builder.ToTable("VitalSigns");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.BloodPressure)
            .HasMaxLength(20);

        builder.Property(v => v.Temperature)
            .HasColumnType("decimal(4,1)");

        builder.Property(v => v.Weight)
            .HasColumnType("decimal(5,2)");

        builder.Property(v => v.Height)
            .HasColumnType("decimal(5,2)");

        builder.HasOne(v => v.MedicalConsultation)
            .WithMany(m => m.VitalSigns)
            .HasForeignKey(v => v.MedicalConsultationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
