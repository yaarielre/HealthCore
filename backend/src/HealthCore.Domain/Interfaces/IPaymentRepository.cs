using HealthCore.Domain.Entities;

namespace HealthCore.Domain.Interfaces;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<IEnumerable<Payment>> GetByInvoiceIdAsync(Guid invoiceId);
    Task<decimal> GetTotalPaidByInvoiceIdAsync(Guid invoiceId);
}
