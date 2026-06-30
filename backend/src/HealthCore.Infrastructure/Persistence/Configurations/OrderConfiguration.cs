using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.Status)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(OrderStatus.Pending)
            .HasSentinel(OrderStatus.Pending);

        builder.Property(o => o.Priority)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(OrderPriority.Normal)
            .HasSentinel(OrderPriority.Normal);

        builder.Property(o => o.Notes)
            .HasMaxLength(1000);

        builder.Property(o => o.OrderedAt)
            .IsRequired();

        builder.Property(o => o.CompletedAt);

        builder.Property(o => o.CancelledAt);

        builder.HasIndex(o => o.OrderNumber)
            .IsUnique();

        builder.HasIndex(o => o.PatientId);
        builder.HasIndex(o => o.DoctorId);
        builder.HasIndex(o => o.Status);

        builder.HasOne(o => o.Patient)
            .WithMany()
            .HasForeignKey(o => o.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Doctor)
            .WithMany()
            .HasForeignKey(o => o.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.MedicalConsultation)
            .WithMany()
            .HasForeignKey(o => o.MedicalConsultationId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(o => o.OrderType)
            .WithMany()
            .HasForeignKey(o => o.OrderTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
