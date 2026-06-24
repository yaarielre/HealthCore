using MediatR;

namespace HealthCore.Application.Features.Specialties.Commands.ToggleSpecialtyStatus;

public record ToggleSpecialtyStatusCommand(Guid Id) : IRequest<bool>;