using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IPrescriptionRepository : IGenericRepository<Prescription>
{
    Task<IEnumerable<Prescription>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<Prescription>> GetByMedicalConsultationIdAsync(Guid consultationId);
}
