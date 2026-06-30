using HealthCore.Application.Features.SystemConfigurations.DTOs;
using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Commands.UpdateSystemConfiguration;

public record UpdateSystemConfigurationCommand(
    Guid Id,
    string Category,
    string ConfigKey,
    string ConfigValue,
    string? Description,
    bool IsEncrypted) : IRequest<SystemConfigurationDto>;
