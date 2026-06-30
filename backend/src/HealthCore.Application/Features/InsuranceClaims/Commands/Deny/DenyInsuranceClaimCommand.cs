using HealthCore.Application.Features.InsuranceClaims.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Commands.Deny;

public record DenyInsuranceClaimCommand(Guid Id, string? Notes) : IRequest<InsuranceClaimDto>;
