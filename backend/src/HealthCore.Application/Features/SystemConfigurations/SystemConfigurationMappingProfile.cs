using AutoMapper;
using HealthCore.Application.Features.SystemConfigurations.DTOs;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.SystemConfigurations;

public class SystemConfigurationMappingProfile : Profile
{
    public SystemConfigurationMappingProfile()
    {
        CreateMap<SystemConfiguration, SystemConfigurationDto>();
        CreateMap<CreateSystemConfigurationDto, SystemConfiguration>();
        CreateMap<UpdateSystemConfigurationDto, SystemConfiguration>();
    }
}
