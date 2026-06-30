using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Persistence.Repositories;

public class SystemConfigurationRepository : GenericRepository<SystemConfiguration>, ISystemConfigurationRepository
{
    public SystemConfigurationRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<bool> ExistsByCategoryAndKeyAsync(string category, string configKey, Guid? excludedId = null)
    {
        var query = _context.SystemConfigurations
            .Where(sc => sc.Category.ToLower() == category.ToLower()
                      && sc.ConfigKey.ToLower() == configKey.ToLower());

        if (excludedId.HasValue)
            query = query.Where(sc => sc.Id != excludedId.Value);

        return await query.AnyAsync();
    }

    public async Task<IEnumerable<SystemConfiguration>> GetByCategoryAsync(string category)
    {
        return await _context.SystemConfigurations
            .Where(sc => sc.Category.ToLower() == category.ToLower())
            .ToListAsync();
    }
}
