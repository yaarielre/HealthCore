using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;

public class VitalSign : BaseEntity
{
    public Guid MedicalConsultationId { get; set; }

    public string? BloodPressure { get; set; }
    public decimal? Temperature { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public int? HeartRate { get; set; }
    public int? OxygenSaturation { get; set; }

    public MedicalConsultation MedicalConsultation { get; set; } = null!;
}
