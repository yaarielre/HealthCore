using HealthCore.Application.Features.SystemConfigurations.DTOs;
using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Commands.CreateSystemConfiguration;

public record CreateSystemConfigurationCommand(
    string Category,
    string ConfigKey,
    string ConfigValue,
    string? Description,
    bool IsEncrypted) : IRequest<SystemConfigurationDto>;
