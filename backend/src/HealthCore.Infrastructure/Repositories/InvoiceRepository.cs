using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Persistence.Repositories;

public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<IEnumerable<Invoice>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.Invoices
            .Where(i => i.PatientId == patientId)
            .OrderByDescending(i => i.InvoiceDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetByStatusAsync(InvoiceStatus status)
    {
        return await _context.Invoices
            .Where(i => i.Status == status)
            .OrderByDescending(i => i.InvoiceDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetByDateRangeAsync(DateTime start, DateTime end)
    {
        return await _context.Invoices
            .Where(i => i.InvoiceDate >= start && i.InvoiceDate <= end)
            .OrderByDescending(i => i.InvoiceDate)
            .ToListAsync();
    }

    public async Task<string> GenerateInvoiceNumberAsync()
    {
        var year = DateTime.UtcNow.Year;
        var month = DateTime.UtcNow.Month;
        var prefix = $"INV-{year}{month:D2}-";

        var lastInvoice = await _context.Invoices
            .Where(i => i.InvoiceNumber.StartsWith(prefix))
            .OrderByDescending(i => i.InvoiceNumber)
            .FirstOrDefaultAsync();

        int nextNumber = 1;
        if (lastInvoice != null)
        {
            var parts = lastInvoice.InvoiceNumber.Split('-');
            if (parts.Length == 3 && int.TryParse(parts[2], out int lastNum))
                nextNumber = lastNum + 1;
        }

        return $"{prefix}{nextNumber:D4}";
    }

    public async Task<Invoice?> GetWithItemsAsync(Guid id)
    {
        return await _context.Invoices
            .Include(i => i.InvoiceItems)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
