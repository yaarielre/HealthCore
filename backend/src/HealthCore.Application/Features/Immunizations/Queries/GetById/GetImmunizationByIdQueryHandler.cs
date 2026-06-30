using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Immunizations.Queries.GetById;

public class GetImmunizationByIdQueryHandler : IRequestHandler<GetImmunizationByIdQuery, ImmunizationDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetImmunizationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ImmunizationDto?> Handle(GetImmunizationByIdQuery request, CancellationToken cancellationToken)
    {
        var immunization = await _unitOfWork.Immunizations.GetByIdAsync(request.Id);
        return immunization is null ? null : _mapper.Map<ImmunizationDto>(immunization);
    }
}
