using AutoMapper;
using HealthCore.Application.Features.SystemConfigurations.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Commands.CreateSystemConfiguration;

public class CreateSystemConfigurationCommandHandler : IRequestHandler<CreateSystemConfigurationCommand, SystemConfigurationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSystemConfigurationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SystemConfigurationDto> Handle(CreateSystemConfigurationCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.SystemConfigurations.ExistsByCategoryAndKeyAsync(request.Category, request.ConfigKey))
            throw new ApplicationException($"Ya existe una configuración con la categoría '{request.Category}' y clave '{request.ConfigKey}'.");

        var config = new SystemConfiguration
        {
            Category = request.Category,
            ConfigKey = request.ConfigKey,
            ConfigValue = request.ConfigValue,
            Description = request.Description,
            IsEncrypted = request.IsEncrypted,
            IsActive = true
        };

        await _unitOfWork.SystemConfigurations.AddAsync(config);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SystemConfigurationDto>(config);
    }
}
