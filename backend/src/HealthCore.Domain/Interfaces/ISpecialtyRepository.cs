using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;
    public interface ISpecialtyRepository : IGenericRepository<Specialty>
    {
        Task<bool> ExistsByNameAsync(string name, Guid? excludedId = null);
    }
