using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;

namespace HealthCore.Application.Interfaces;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date);
    Task<bool> HasConflictAsync(Guid doctorId, DateTime appointmentDate, Guid? excludeId = null);
}