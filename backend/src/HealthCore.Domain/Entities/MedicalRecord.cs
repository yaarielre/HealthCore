using HealthCore.Domain.Common;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Entities;

public class MedicalRecord : BaseEntity
{
    public Guid PatientId { get; set; }
    public string RecordNumber { get; set; } = string.Empty;
    public BloodType? BloodType { get; set; }
    public string? Allergies { get; set; }
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhone { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;

    public Patient Patient { get; set; } = null!;
}
