using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class MedicalRecordRepository : GenericRepository<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(HealthCoreDbContext context) : base(context) { }

    public override async Task<IEnumerable<MedicalRecord>> GetAllAsync() =>
        await _dbSet
            .Include(mr => mr.Patient)
            .OrderByDescending(mr => mr.CreatedAt)
            .ToListAsync();

    public override async Task<MedicalRecord?> GetByIdAsync(Guid id) =>
        await _dbSet
            .Include(mr => mr.Patient)
            .FirstOrDefaultAsync(mr => mr.Id == id);

    public async Task<MedicalRecord?> GetByPatientIdAsync(Guid patientId) =>
        await _dbSet
            .Include(mr => mr.Patient)
            .FirstOrDefaultAsync(mr => mr.PatientId == patientId);
}
