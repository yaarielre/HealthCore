using AutoMapper;
using HealthCore.Application.Features.SystemConfigurations.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Queries.GetAllSystemConfigurations;

public class GetAllSystemConfigurationsQueryHandler : IRequestHandler<GetAllSystemConfigurationsQuery, IEnumerable<SystemConfigurationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllSystemConfigurationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SystemConfigurationDto>> Handle(GetAllSystemConfigurationsQuery request, CancellationToken cancellationToken)
    {
        var configs = await _unitOfWork.SystemConfigurations.GetAllAsync();
        return _mapper.Map<IEnumerable<SystemConfigurationDto>>(configs);
    }
}
