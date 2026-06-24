namespace HealthCore.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IAppointmentRepository Appointments { get; }
    ISpecialtyRepository Specialties { get; }
    IDoctorRepository Doctors { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}