using HealthCore.Application.Features.InsuranceClaims.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Queries.GetByPatient;

public record GetInsuranceClaimsByPatientQuery(Guid PatientId) : IRequest<IEnumerable<InsuranceClaimDto>>;
