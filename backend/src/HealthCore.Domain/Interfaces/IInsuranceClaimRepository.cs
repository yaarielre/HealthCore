using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Interfaces;

public interface IInsuranceClaimRepository : IGenericRepository<InsuranceClaim>
{
    Task<IEnumerable<InsuranceClaim>> GetByInvoiceIdAsync(Guid invoiceId);
    Task<IEnumerable<InsuranceClaim>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<InsuranceClaim>> GetByStatusAsync(ClaimStatus status);
}
