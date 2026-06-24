using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Persistence.Repositories;

public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(HealthCoreDbContext context) : base(context) { }

    public override async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return await _context.Doctors
            .Include(d => d.Specialty)
            .ToListAsync();
    }

    public override async Task<Doctor?> GetByIdAsync(Guid id)
    {
        return await _context.Doctors
            .Include(d => d.Specialty)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<bool> ExistsByLicenseNumberAsync(string licenseNumber, Guid? excludeId = null)
    {
        var query = _context.Doctors.Where(d => d.LicenseNumber == licenseNumber);
        if (excludeId.HasValue)
            query = query.Where(d => d.Id != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<IEnumerable<Doctor>> GetBySpecialtyAsync(Guid specialtyId)
    {
        return await _context.Doctors
            .Include(d => d.Specialty)
            .Where(d => d.SpecialtyId == specialtyId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Doctor>> GetActiveDoctorsAsync()
    {
        return await _context.Doctors
            .Include(d => d.Specialty)
            .Where(d => d.IsActive)
            .ToListAsync();
    }
}