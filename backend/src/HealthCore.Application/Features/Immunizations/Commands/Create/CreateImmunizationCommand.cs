using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;

namespace HealthCore.Application.Features.Immunizations.Commands.Create;

public record CreateImmunizationCommand(CreateImmunizationDto Dto) : IRequest<ImmunizationDto>;
