// .../GetAllSpecialtiesQueryHandler.cs
using AutoMapper;
using HealthCore.Application.Features.Specialties.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Specialties.Queries.GetAllSpecialties;

public class GetAllSpecialtiesQueryHandler : IRequestHandler<GetAllSpecialtiesQuery, IEnumerable<SpecialtyDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllSpecialtiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SpecialtyDto>> Handle(GetAllSpecialtiesQuery request, CancellationToken cancellationToken)
    {
        var specialties = await _unitOfWork.Specialties.GetAllAsync();
        return _mapper.Map<IEnumerable<SpecialtyDto>>(specialties);
    }
}