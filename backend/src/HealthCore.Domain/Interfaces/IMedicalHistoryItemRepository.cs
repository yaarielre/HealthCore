using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IMedicalHistoryItemRepository : IGenericRepository<MedicalHistoryItem>
{
    Task<IEnumerable<MedicalHistoryItem>> GetByPatientIdAsync(Guid patientId);
}
