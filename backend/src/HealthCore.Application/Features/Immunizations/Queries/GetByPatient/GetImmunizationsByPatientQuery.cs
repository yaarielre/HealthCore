using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;

namespace HealthCore.Application.Features.Immunizations.Queries.GetByPatient;

public record GetImmunizationsByPatientQuery(Guid PatientId) : IRequest<IEnumerable<ImmunizationDto>>;
