using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class ImmunizationRepository : GenericRepository<Immunization>, IImmunizationRepository
{
    public ImmunizationRepository(HealthCoreDbContext context) : base(context) { }

    public override async Task<IEnumerable<Immunization>> GetAllAsync() =>
        await _dbSet
            .Include(i => i.Patient)
            .OrderByDescending(i => i.ApplicationDate)
            .ToListAsync();

    public override async Task<Immunization?> GetByIdAsync(Guid id) =>
        await _dbSet
            .Include(i => i.Patient)
            .FirstOrDefaultAsync(i => i.Id == id);

    public async Task<IEnumerable<Immunization>> GetByPatientIdAsync(Guid patientId) =>
        await _dbSet
            .Where(i => i.PatientId == patientId)
            .OrderByDescending(i => i.ApplicationDate)
            .ToListAsync();
}
