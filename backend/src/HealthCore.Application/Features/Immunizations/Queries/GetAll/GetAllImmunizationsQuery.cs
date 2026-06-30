using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;

namespace HealthCore.Application.Features.Immunizations.Queries.GetAll;

public record GetAllImmunizationsQuery : IRequest<IEnumerable<ImmunizationDto>>;
