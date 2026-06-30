using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IOrderTypeRepository : IGenericRepository<OrderType>
{
    Task<OrderType?> GetByNameAsync(string name);
}
