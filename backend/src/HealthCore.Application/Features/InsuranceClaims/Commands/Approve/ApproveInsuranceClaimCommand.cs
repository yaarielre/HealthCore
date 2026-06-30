using HealthCore.Application.Features.InsuranceClaims.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Commands.Approve;

public record ApproveInsuranceClaimCommand(Guid Id, decimal ApprovedAmount) : IRequest<InsuranceClaimDto>;
