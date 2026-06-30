using MediatR;

namespace HealthCore.Application.Features.Immunizations.Commands.Delete;

public record DeleteImmunizationCommand(Guid Id) : IRequest<bool>;
