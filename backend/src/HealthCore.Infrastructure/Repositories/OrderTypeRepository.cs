using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class OrderTypeRepository : GenericRepository<OrderType>, IOrderTypeRepository
{
    public OrderTypeRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<OrderType?> GetByNameAsync(string name)
    {
        return await _context.OrderTypes
            .FirstOrDefaultAsync(ot => ot.Name == name);
    }
}
