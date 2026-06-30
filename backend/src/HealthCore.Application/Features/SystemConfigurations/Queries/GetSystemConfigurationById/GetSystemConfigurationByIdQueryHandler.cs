using AutoMapper;
using HealthCore.Application.Features.SystemConfigurations.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Queries.GetSystemConfigurationById;

public class GetSystemConfigurationByIdQueryHandler : IRequestHandler<GetSystemConfigurationByIdQuery, SystemConfigurationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSystemConfigurationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SystemConfigurationDto> Handle(GetSystemConfigurationByIdQuery request, CancellationToken cancellationToken)
    {
        var config = await _unitOfWork.SystemConfigurations.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Configuración con Id {request.Id} no encontrada.");

        return _mapper.Map<SystemConfigurationDto>(config);
    }
}
