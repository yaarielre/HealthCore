using HealthCore.Application.Interfaces;
using HealthCore.Infrastructure.Persistence;

namespace HealthCore.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly HealthCoreDbContext _context;

    public IUserRepository Users { get; }
    public IAppointmentRepository Appointments { get; }

    public UnitOfWork(HealthCoreDbContext context, IUserRepository users, IAppointmentRepository appointments)
    {
        _context = context;
        Users = users;
        Appointments = appointments;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);

    public void Dispose() => _context.Dispose();
}