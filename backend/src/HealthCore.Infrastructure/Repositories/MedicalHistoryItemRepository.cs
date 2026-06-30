using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class MedicalHistoryItemRepository : GenericRepository<MedicalHistoryItem>, IMedicalHistoryItemRepository
{
    public MedicalHistoryItemRepository(HealthCoreDbContext context) : base(context) { }

    public override async Task<IEnumerable<MedicalHistoryItem>> GetAllAsync() =>
        await _dbSet
            .Include(mhi => mhi.Patient)
            .Include(mhi => mhi.RecordedBy)
            .OrderByDescending(mhi => mhi.CreatedAt)
            .ToListAsync();

    public override async Task<MedicalHistoryItem?> GetByIdAsync(Guid id) =>
        await _dbSet
            .Include(mhi => mhi.Patient)
            .Include(mhi => mhi.RecordedBy)
            .FirstOrDefaultAsync(mhi => mhi.Id == id);

    public async Task<IEnumerable<MedicalHistoryItem>> GetByPatientIdAsync(Guid patientId) =>
        await _dbSet
            .Include(mhi => mhi.RecordedBy)
            .Where(mhi => mhi.PatientId == patientId)
            .OrderByDescending(mhi => mhi.RecordedDate)
            .ThenByDescending(mhi => mhi.CreatedAt)
            .ToListAsync();
}
