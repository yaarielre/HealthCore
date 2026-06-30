using HealthCore.Domain.Common;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Entities;

public class InsuranceClaim : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public Guid PatientId { get; set; }
    public string InsuranceCompany { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public string? ClaimNumber { get; set; }
    public decimal ClaimAmount { get; set; }
    public decimal? ApprovedAmount { get; set; }
    public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
    public DateTime FiledAt { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovedAt { get; set; }
    public string? Notes { get; set; }

    public Invoice Invoice { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
}
