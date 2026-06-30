using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId);
    Task<IEnumerable<OrderItem>> GetByPatientIdAsync(Guid patientId);
}
