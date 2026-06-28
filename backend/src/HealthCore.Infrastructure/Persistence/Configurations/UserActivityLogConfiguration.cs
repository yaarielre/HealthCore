using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class UserActivityLogConfiguration : IEntityTypeConfiguration<UserActivityLog>
{
    public void Configure(EntityTypeBuilder<UserActivityLog> builder)
    {
        builder.ToTable("UserActivityLogs");

        builder.HasKey(al => al.Id);

        builder.Property(al => al.Action)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(al => al.Module)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(al => al.Details)
            .HasMaxLength(2000);

        builder.Property(al => al.IpAddress)
            .HasMaxLength(50);

        builder.HasIndex(al => al.UserId);
        builder.HasIndex(al => al.Action);
        builder.HasIndex(al => al.Module);

        builder.HasOne(al => al.User)
            .WithMany(u => u.ActivityLogs)
            .HasForeignKey(al => al.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
