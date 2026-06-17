using HealthCore.Application.Interfaces;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId) =>
        await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorId == doctorId)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId) =>
        await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date) =>
        await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.AppointmentDate.Date == date.Date)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<bool> HasConflictAsync(Guid doctorId, DateTime appointmentDate, Guid? excludeId = null)
    {
        var window = TimeSpan.FromMinutes(30);
        var start = appointmentDate.Subtract(window);
        var end = appointmentDate.Add(window);

        return await _dbSet.AnyAsync(a =>
            a.DoctorId == doctorId &&
            a.Status != AppointmentStatus.Cancelled &&
            a.Status != AppointmentStatus.NoShow &&
            (excludeId == null || a.Id != excludeId) &&
            a.AppointmentDate > start &&
            a.AppointmentDate < end);
    }
}
