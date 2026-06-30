using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;

public class OrderType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}
