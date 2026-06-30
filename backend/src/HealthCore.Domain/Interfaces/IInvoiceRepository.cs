using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Interfaces;

public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    Task<IEnumerable<Invoice>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<Invoice>> GetByStatusAsync(InvoiceStatus status);
    Task<IEnumerable<Invoice>> GetByDateRangeAsync(DateTime start, DateTime end);
    Task<string> GenerateInvoiceNumberAsync();
    Task<Invoice?> GetWithItemsAsync(Guid id);
}
