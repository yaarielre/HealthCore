using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface ISystemConfigurationRepository : IGenericRepository<SystemConfiguration>
{
    Task<bool> ExistsByCategoryAndKeyAsync(string category, string configKey, Guid? excludedId = null);
    Task<IEnumerable<SystemConfiguration>> GetByCategoryAsync(string category);
}
