using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;

namespace HealthCore.Application.Features.Immunizations.Queries.GetById;

public record GetImmunizationByIdQuery(Guid Id) : IRequest<ImmunizationDto?>;
