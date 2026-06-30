using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class MedicalImageConfiguration : IEntityTypeConfiguration<MedicalImage>
{
    public void Configure(EntityTypeBuilder<MedicalImage> builder)
    {
        builder.ToTable("MedicalImages");

        builder.HasKey(mi => mi.Id);

        builder.Property(mi => mi.ImageType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(mi => mi.BodyPart)
            .HasMaxLength(100);

        builder.Property(mi => mi.FileName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(mi => mi.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(mi => mi.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(mi => mi.FileSizeBytes)
            .IsRequired()
            .HasDefaultValue(0L);

        builder.Property(mi => mi.Description)
            .HasMaxLength(500);

        builder.Property(mi => mi.Interpretation)
            .HasMaxLength(2000);

        builder.Property(mi => mi.TakenAt);

        builder.HasIndex(mi => mi.PatientId);
        builder.HasIndex(mi => mi.MedicalConsultationId);

        builder.HasOne(mi => mi.Patient)
            .WithMany()
            .HasForeignKey(mi => mi.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mi => mi.MedicalConsultation)
            .WithMany()
            .HasForeignKey(mi => mi.MedicalConsultationId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(mi => mi.OrderItem)
            .WithMany()
            .HasForeignKey(mi => mi.OrderItemId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(mi => mi.InterpretedBy)
            .WithMany()
            .HasForeignKey(mi => mi.InterpretedById)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
