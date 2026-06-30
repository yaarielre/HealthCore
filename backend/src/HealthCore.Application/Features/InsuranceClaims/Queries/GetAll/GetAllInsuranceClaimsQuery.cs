using HealthCore.Application.Features.InsuranceClaims.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Queries.GetAll;

public record GetAllInsuranceClaimsQuery : IRequest<IEnumerable<InsuranceClaimDto>>;
