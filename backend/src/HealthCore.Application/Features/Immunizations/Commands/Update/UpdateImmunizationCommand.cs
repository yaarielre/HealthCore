using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;

namespace HealthCore.Application.Features.Immunizations.Commands.Update;

public record UpdateImmunizationCommand(Guid Id, UpdateImmunizationDto Dto) : IRequest<ImmunizationDto>;
