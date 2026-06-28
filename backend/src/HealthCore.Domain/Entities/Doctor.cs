using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;

public class Doctor : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public Guid SpecialtyId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navegación
    public Specialty Specialty { get; set; } = null!;
    public User? User { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<MedicalConsultation> MedicalConsultations { get; set; } = new List<MedicalConsultation>();
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}