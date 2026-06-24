using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using HealthCore.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly HealthCoreDbContext _context;

    public IUserRepository Users { get; }
    public ISpecialtyRepository Specialties { get; }
    public IDoctorRepository Doctors { get; }
    public IAppointmentRepository Appointments { get; }

    public UnitOfWork(
        HealthCoreDbContext context,
        IUserRepository users,
        IAppointmentRepository appointments)
    {
        _context = context;

        Users = users;
        Appointments = appointments;

        Specialties = new SpecialtyRepository(_context);
        Doctors = new DoctorRepository(_context);
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}