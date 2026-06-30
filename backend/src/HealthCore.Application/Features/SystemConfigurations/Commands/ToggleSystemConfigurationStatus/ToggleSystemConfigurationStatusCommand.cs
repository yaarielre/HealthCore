using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Commands.ToggleSystemConfigurationStatus;

public record ToggleSystemConfigurationStatusCommand(Guid Id) : IRequest<bool>;
