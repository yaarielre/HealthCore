using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<Order>> GetByDoctorIdAsync(Guid doctorId);
    Task<Order?> GetWithItemsAsync(Guid id);
}
