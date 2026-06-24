
using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<bool> ExistsByLicenseNumberAsync(string licenseNumber, Guid? EcludedId = null);
        Task<IEnumerable<Doctor>> GetBySpecialtyAsync(Guid specialtyId);
        Task<IEnumerable<Doctor>> GetActiveDoctorsAsync();
    }