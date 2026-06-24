using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Interfaces;
public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdNumberAsync(string idNumber);
    Task<IEnumerable<User>> GetByRoleAsync(UserRole role);
    Task<bool> EmailExistsAsync(string email);
    Task<IEnumerable<UserActivityLog>> GetLogsAsync();
}