using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Immunizations.Queries.GetAll;

public class GetAllImmunizationsQueryHandler : IRequestHandler<GetAllImmunizationsQuery, IEnumerable<ImmunizationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllImmunizationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ImmunizationDto>> Handle(GetAllImmunizationsQuery request, CancellationToken cancellationToken)
    {
        var immunizations = await _unitOfWork.Immunizations.GetAllAsync();
        return _mapper.Map<IEnumerable<ImmunizationDto>>(immunizations);
    }
}
