using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IMedicalConsultationRepository : IGenericRepository<MedicalConsultation>
{
    Task<IEnumerable<MedicalConsultation>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<MedicalConsultation>> GetByDoctorIdAsync(Guid doctorId);
    Task<MedicalConsultation?> GetWithDetailsAsync(Guid id);
}
