using HealthCore.Domain.Enums;

namespace HealthCore.Application.Features.InsuranceClaims.DTOs;

public class InsuranceClaimDto
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public string? InvoiceNumber { get; set; }
    public Guid PatientId { get; set; }
    public string? PatientName { get; set; }
    public string InsuranceCompany { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public string? ClaimNumber { get; set; }
    public decimal ClaimAmount { get; set; }
    public decimal? ApprovedAmount { get; set; }
    public ClaimStatus Status { get; set; }
    public DateTime FiledAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateInsuranceClaimDto
{
    public Guid InvoiceId { get; set; }
    public Guid PatientId { get; set; }
    public string InsuranceCompany { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public decimal ClaimAmount { get; set; }
    public string? Notes { get; set; }
}

public class UpdateInsuranceClaimDto
{
    public string InsuranceCompany { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public decimal ClaimAmount { get; set; }
    public string? Notes { get; set; }
}
