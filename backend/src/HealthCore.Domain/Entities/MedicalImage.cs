using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;

public class MedicalImage : BaseEntity
{
    public Guid PatientId { get; set; }
    public Guid? MedicalConsultationId { get; set; }
    public Guid? OrderItemId { get; set; }
    public string ImageType { get; set; } = string.Empty;
    public string? BodyPart { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public string? Description { get; set; }
    public string? Interpretation { get; set; }
    public Guid? InterpretedById { get; set; }
    public DateTime? TakenAt { get; set; }

    public Patient Patient { get; set; } = null!;
    public MedicalConsultation? MedicalConsultation { get; set; }
    public OrderItem? OrderItem { get; set; }
    public User? InterpretedBy { get; set; }
}
