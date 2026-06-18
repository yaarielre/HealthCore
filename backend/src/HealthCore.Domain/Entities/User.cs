using HealthCore.Domain.Common;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public AccountStatus Status { get; set; } = AccountStatus.Active;
    public Guid? DoctorId { get; set; }

    public Doctor? Doctor { get; set; }
    public ICollection<UserActivityLog> ActivityLogs { get; set; } = new List<UserActivityLog>();
}
