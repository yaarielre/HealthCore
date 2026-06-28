using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IPatientRepository : IGenericRepository<Patient>
{
    Task<IEnumerable<Patient>> SearchAsync(string term);
    Task<Patient?> GetByIdNumberAsync(string idNumber);
    Task<bool> ExistsByIdNumberAsync(string idNumber, Guid? excludeId = null);
    Task<Patient?> GetWithDetailsAsync(Guid id);
    Task<IEnumerable<Patient>> GetAllWithPaginationAsync(int page, int pageSize);
}