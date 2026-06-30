using AutoMapper;
using HealthCore.Application.Features.SystemConfigurations.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Commands.UpdateSystemConfiguration;

public class UpdateSystemConfigurationCommandHandler : IRequestHandler<UpdateSystemConfigurationCommand, SystemConfigurationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSystemConfigurationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SystemConfigurationDto> Handle(UpdateSystemConfigurationCommand request, CancellationToken cancellationToken)
    {
        var config = await _unitOfWork.SystemConfigurations.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Configuración con Id {request.Id} no encontrada.");

        if (await _unitOfWork.SystemConfigurations.ExistsByCategoryAndKeyAsync(request.Category, request.ConfigKey, request.Id))
            throw new ApplicationException($"Ya existe otra configuración con la categoría '{request.Category}' y clave '{request.ConfigKey}'.");

        config.Category = request.Category;
        config.ConfigKey = request.ConfigKey;
        config.ConfigValue = request.ConfigValue;
        config.Description = request.Description;
        config.IsEncrypted = request.IsEncrypted;

        await _unitOfWork.SystemConfigurations.UpdateAsync(config);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SystemConfigurationDto>(config);
    }
}
