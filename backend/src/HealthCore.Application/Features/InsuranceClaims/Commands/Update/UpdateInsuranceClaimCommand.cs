using HealthCore.Application.Features.InsuranceClaims.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Commands.Update;

public record UpdateInsuranceClaimCommand(
    Guid Id,
    string InsuranceCompany,
    string PolicyNumber,
    decimal ClaimAmount,
    string? Notes) : IRequest<InsuranceClaimDto>;
