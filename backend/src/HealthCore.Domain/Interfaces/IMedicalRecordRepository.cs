using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IMedicalRecordRepository : IGenericRepository<MedicalRecord>
{
    Task<MedicalRecord?> GetByPatientIdAsync(Guid patientId);
}
