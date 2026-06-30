using HealthCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCore.Infrastructure.Persistence.Configurations;

public class SystemConfigurationConfiguration : IEntityTypeConfiguration<SystemConfiguration>
{
    public void Configure(EntityTypeBuilder<SystemConfiguration> builder)
    {
        builder.ToTable("SystemConfigurations");

        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(sc => sc.ConfigKey)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(sc => sc.ConfigValue)
            .IsRequired();

        builder.Property(sc => sc.Description)
            .HasMaxLength(500);

        builder.Property(sc => sc.IsEncrypted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(sc => sc.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(sc => new { sc.Category, sc.ConfigKey })
            .IsUnique();

        builder.HasIndex(sc => sc.Category);
    }
}
