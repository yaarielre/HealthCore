using HealthCore.Application.Features.SystemConfigurations.DTOs;
using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Queries.GetSystemConfigurationById;

public record GetSystemConfigurationByIdQuery(Guid Id) : IRequest<SystemConfigurationDto>;
