using HealthCore.Application.Features.InsuranceClaims.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Commands.Create;

public record CreateInsuranceClaimCommand(
    Guid InvoiceId,
    Guid PatientId,
    string InsuranceCompany,
    string PolicyNumber,
    decimal ClaimAmount,
    string? Notes) : IRequest<InsuranceClaimDto>;
