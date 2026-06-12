using HealthCore.Application.Interfaces;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email) =>
        await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdNumberAsync(string idNumber) =>
        await _dbSet.FirstOrDefaultAsync(u => u.IdNumber == idNumber);

    public async Task<IEnumerable<User>> GetByRoleAsync(UserRole role) =>
        await _dbSet.Where(u => u.Role == role).ToListAsync();

    public async Task<bool> EmailExistsAsync(string email) =>
        await _dbSet.AnyAsync(u => u.Email == email);
}