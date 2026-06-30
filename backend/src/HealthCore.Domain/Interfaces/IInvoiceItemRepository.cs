using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IInvoiceItemRepository : IGenericRepository<InvoiceItem>
{
    Task<IEnumerable<InvoiceItem>> GetByInvoiceIdAsync(Guid invoiceId);
}
