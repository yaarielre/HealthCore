using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;

public class SystemConfiguration : BaseEntity
{
    public string Category { get; set; } = string.Empty;
    public string ConfigKey { get; set; } = string.Empty;
    public string ConfigValue { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsEncrypted { get; set; }
    public bool IsActive { get; set; } = true;
}
