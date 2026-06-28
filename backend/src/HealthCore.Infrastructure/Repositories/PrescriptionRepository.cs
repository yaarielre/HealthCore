using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
{
    public PrescriptionRepository(HealthCoreDbContext context) : base(context) { }

    public override async Task<IEnumerable<Prescription>> GetAllAsync() =>
        await _dbSet
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

    public override async Task<Prescription?> GetByIdAsync(Guid id) =>
        await _dbSet
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.MedicalConsultation)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(Guid patientId) =>
        await _dbSet
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Where(p => p.PatientId == patientId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<Prescription>> GetByMedicalConsultationIdAsync(Guid consultationId) =>
        await _dbSet
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Where(p => p.MedicalConsultationId == consultationId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
}
