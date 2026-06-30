namespace HealthCore.Application.Features.SystemConfigurations.DTOs;

public class SystemConfigurationDto
{
    public Guid Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string ConfigKey { get; set; } = string.Empty;
    public string ConfigValue { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsEncrypted { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateSystemConfigurationDto
{
    public string Category { get; set; } = string.Empty;
    public string ConfigKey { get; set; } = string.Empty;
    public string ConfigValue { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsEncrypted { get; set; }
}

public class UpdateSystemConfigurationDto
{
    public string Category { get; set; } = string.Empty;
    public string ConfigKey { get; set; } = string.Empty;
    public string ConfigValue { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsEncrypted { get; set; }
}
