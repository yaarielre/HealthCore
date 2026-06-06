using HealthCore.Application.Interfaces;
using HealthCore.Domain.Entities;
using HealthCore.Infrastructure.Persistence;

namespace HealthCore.Infrastructure.Repositories;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(HealthCoreDbContext context) : base(context)
    {
    }
}