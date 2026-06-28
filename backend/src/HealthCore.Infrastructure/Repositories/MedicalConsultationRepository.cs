using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class MedicalConsultationRepository : GenericRepository<MedicalConsultation>, IMedicalConsultationRepository
{
    public MedicalConsultationRepository(HealthCoreDbContext context) : base(context) { }

    public override async Task<IEnumerable<MedicalConsultation>> GetAllAsync() =>
        await _dbSet
            .Include(m => m.Patient)
            .Include(m => m.Doctor)
            .Include(m => m.VitalSigns)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

    public override async Task<MedicalConsultation?> GetByIdAsync(Guid id) =>
        await _dbSet
            .Include(m => m.Patient)
            .Include(m => m.Doctor)
            .Include(m => m.VitalSigns)
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IEnumerable<MedicalConsultation>> GetByPatientIdAsync(Guid patientId) =>
        await _dbSet
            .Include(m => m.Patient)
            .Include(m => m.Doctor)
            .Include(m => m.VitalSigns)
            .Where(m => m.PatientId == patientId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<MedicalConsultation>> GetByDoctorIdAsync(Guid doctorId) =>
        await _dbSet
            .Include(m => m.Patient)
            .Include(m => m.Doctor)
            .Include(m => m.VitalSigns)
            .Where(m => m.DoctorId == doctorId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

    public async Task<MedicalConsultation?> GetWithDetailsAsync(Guid id) =>
        await _dbSet
            .Include(m => m.Patient)
            .Include(m => m.Doctor)
            .ThenInclude(d => d.Specialty)
            .Include(m => m.VitalSigns)
            .Include(m => m.Appointment)
            .FirstOrDefaultAsync(m => m.Id == id);
}
