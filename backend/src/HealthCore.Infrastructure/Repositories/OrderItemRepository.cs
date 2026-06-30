using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId)
    {
        return await _context.OrderItems
            .Where(i => i.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderItem>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.OrderItems
            .Include(i => i.Order)
            .Where(i => i.Order.PatientId == patientId)
            .ToListAsync();
    }
}
