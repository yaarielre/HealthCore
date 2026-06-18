namespace HealthCore.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IAppointmentRepository Appointments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}