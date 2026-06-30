using HealthCore.Application.Features.SystemConfigurations.DTOs;
using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Queries.GetAllSystemConfigurations;

public record GetAllSystemConfigurationsQuery : IRequest<IEnumerable<SystemConfigurationDto>>;
