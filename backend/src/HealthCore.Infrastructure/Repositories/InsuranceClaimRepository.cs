using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using HealthCore.Domain.Interfaces;
using HealthCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCore.Infrastructure.Persistence.Repositories;

public class InsuranceClaimRepository : GenericRepository<InsuranceClaim>, IInsuranceClaimRepository
{
    public InsuranceClaimRepository(HealthCoreDbContext context) : base(context) { }

    public async Task<IEnumerable<InsuranceClaim>> GetByInvoiceIdAsync(Guid invoiceId)
    {
        return await _context.InsuranceClaims
            .Where(ic => ic.InvoiceId == invoiceId)
            .OrderByDescending(ic => ic.FiledAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<InsuranceClaim>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.InsuranceClaims
            .Where(ic => ic.PatientId == patientId)
            .OrderByDescending(ic => ic.FiledAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<InsuranceClaim>> GetByStatusAsync(ClaimStatus status)
    {
        return await _context.InsuranceClaims
            .Where(ic => ic.Status == status)
            .OrderByDescending(ic => ic.FiledAt)
            .ToListAsync();
    }
}
