using HealthCore.Domain.Common;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Entities;

public class MedicalHistoryItem : BaseEntity
{
    public Guid PatientId { get; set; }
    public MedicalHistoryCategory Category { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Details { get; set; }
    public DateTime? RecordedDate { get; set; }
    public int? Severity { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid RecordedById { get; set; }

    public Patient Patient { get; set; } = null!;
    public User RecordedBy { get; set; } = null!;
}
