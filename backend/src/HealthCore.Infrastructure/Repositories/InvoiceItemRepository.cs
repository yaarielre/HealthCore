using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Persistence.Repositories;

public class InvoiceItemRepository : GenericRepository<InvoiceItem>, IInvoiceItemRepository
{
    public InvoiceItemRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<IEnumerable<InvoiceItem>> GetByInvoiceIdAsync(Guid invoiceId)
    {
        return await _context.InvoiceItems
            .Where(ii => ii.InvoiceId == invoiceId)
            .ToListAsync();
    }
}
