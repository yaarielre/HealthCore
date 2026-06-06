using HealthCore.Application.Interfaces;
using HealthCore.Infrastructure.Persistence;

namespace HealthCore.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly HealthCoreDbContext _context;

    public UnitOfWork(HealthCoreDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);

    public void Dispose() => _context.Dispose();
}