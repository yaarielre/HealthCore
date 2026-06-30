using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IImmunizationRepository : IGenericRepository<Immunization>
{
    Task<IEnumerable<Immunization>> GetByPatientIdAsync(Guid patientId);
}
