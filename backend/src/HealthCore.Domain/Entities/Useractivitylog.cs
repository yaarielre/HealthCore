using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;
public class UserActivityLog : BaseEntity
{
    public Guid UserId { get; set; }
    public string Action { get; set; } = string.Empty; 
    public string Module { get; set; } = string.Empty; 
    public string? Details { get; set; }
    public string? IpAddress { get; set; }

    public User User { get; set; } = null!;
}
