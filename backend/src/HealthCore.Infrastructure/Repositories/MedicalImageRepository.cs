using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class MedicalImageRepository : GenericRepository<MedicalImage>, IMedicalImageRepository
{
    public MedicalImageRepository(HealthCoreDbContext context) : base(context) { }

    public override async Task<IEnumerable<MedicalImage>> GetAllAsync() =>
        await _dbSet
            .Include(mi => mi.Patient)
            .OrderByDescending(mi => mi.CreatedAt)
            .ToListAsync();

    public override async Task<MedicalImage?> GetByIdAsync(Guid id) =>
        await _dbSet
            .Include(mi => mi.Patient)
            .Include(mi => mi.MedicalConsultation)
            .Include(mi => mi.OrderItem)
            .Include(mi => mi.InterpretedBy)
            .FirstOrDefaultAsync(mi => mi.Id == id);

    public async Task<IEnumerable<MedicalImage>> GetByPatientIdAsync(Guid patientId) =>
        await _dbSet
            .Include(mi => mi.MedicalConsultation)
            .Include(mi => mi.InterpretedBy)
            .Where(mi => mi.PatientId == patientId)
            .OrderByDescending(mi => mi.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<MedicalImage>> GetByConsultationIdAsync(Guid consultationId) =>
        await _dbSet
            .Include(mi => mi.InterpretedBy)
            .Where(mi => mi.MedicalConsultationId == consultationId)
            .OrderByDescending(mi => mi.CreatedAt)
            .ToListAsync();
}
