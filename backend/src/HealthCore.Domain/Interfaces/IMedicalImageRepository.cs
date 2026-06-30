using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IMedicalImageRepository : IGenericRepository<MedicalImage>
{
    Task<IEnumerable<MedicalImage>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<MedicalImage>> GetByConsultationIdAsync(Guid consultationId);
}
