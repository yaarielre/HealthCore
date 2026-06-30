using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Persistence.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<IEnumerable<Payment>> GetByInvoiceIdAsync(Guid invoiceId)
    {
        return await _context.Payments
            .Where(p => p.InvoiceId == invoiceId)
            .OrderByDescending(p => p.PaymentDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalPaidByInvoiceIdAsync(Guid invoiceId)
    {
        return await _context.Payments
            .Where(p => p.InvoiceId == invoiceId)
            .SumAsync(p => p.Amount);
    }
}
