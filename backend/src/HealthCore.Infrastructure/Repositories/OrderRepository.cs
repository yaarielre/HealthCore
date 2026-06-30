using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.Orders
            .Include(o => o.OrderType)
            .Include(o => o.OrderItems)
            .Where(o => o.PatientId == patientId)
            .OrderByDescending(o => o.OrderedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByDoctorIdAsync(Guid doctorId)
    {
        return await _context.Orders
            .Include(o => o.OrderType)
            .Include(o => o.OrderItems)
            .Where(o => o.DoctorId == doctorId)
            .OrderByDescending(o => o.OrderedAt)
            .ToListAsync();
    }

    public async Task<Order?> GetWithItemsAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.OrderType)
            .Include(o => o.OrderItems)
            .Include(o => o.Patient)
            .Include(o => o.Doctor)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
