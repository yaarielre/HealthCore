using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;

public class Immunization : BaseEntity
{
    public Guid PatientId { get; set; }
    public string VaccineName { get; set; } = string.Empty;
    public int DoseNumber { get; set; } = 1;
    public DateTime ApplicationDate { get; set; }
    public DateTime? NextDoseDate { get; set; }
    public string? BatchNumber { get; set; }
    public string? AdministeredBy { get; set; }
    public string? Notes { get; set; }

    public Patient Patient { get; set; } = null!;
}
