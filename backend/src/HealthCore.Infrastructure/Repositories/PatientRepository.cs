using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(HealthCoreDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Patient>> SearchAsync(string term) =>
        await _dbSet.Where(p => p.FirstName.Contains(term) || p.LastName.Contains(term) || p.IdNumber.Contains(term) || p.Phone.Contains(term))
            .OrderBy(p => p.FirstName)
            .ToListAsync();

    public async Task<Patient?> GetByIdNumberAsync(string idNumber) =>
        await _dbSet.FirstOrDefaultAsync(p => p.IdNumber == idNumber);

    public async Task<bool> ExistsByIdNumberAsync(string idNumber, Guid? excludeId = null) =>
        excludeId.HasValue
            ? await _dbSet.AnyAsync(p => p.IdNumber == idNumber && p.Id != excludeId.Value)
            : await _dbSet.AnyAsync(p => p.IdNumber == idNumber);

    public async Task<Patient?> GetWithDetailsAsync(Guid id) =>
        await _dbSet
            .Include(p => p.Appointments)
                .ThenInclude(a => a.Doctor)
            .Include(p => p.MedicalConsultations)
                .ThenInclude(c => c.Doctor)
            .Include(p => p.Prescriptions)
                .ThenInclude(r => r.Doctor)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Patient>> GetAllWithPaginationAsync(int page, int pageSize) =>
        await _dbSet
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
}