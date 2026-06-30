using HealthCore.Application.Features.InsuranceClaims.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Queries.GetById;

public record GetInsuranceClaimByIdQuery(Guid Id) : IRequest<InsuranceClaimDto>;
