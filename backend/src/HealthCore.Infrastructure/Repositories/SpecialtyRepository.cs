using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Persistence.Repositories;

public class SpecialtyRepository : GenericRepository<Specialty>, ISpecialtyRepository
{
    public SpecialtyRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null)
    {
        var query = _context.Specialties.Where(s => s.Name.ToLower() == name.ToLower());
        if (excludeId.HasValue)
            query = query.Where(s => s.Id != excludeId.Value);
        return await query.AnyAsync();
    }
}