using AutoMapper;
using HealthCore.Application.Features.Specialties.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Specialties.Queries.GetSpecialtyById;

public class GetSpecialtyByIdQueryHandler : IRequestHandler<GetSpecialtyByIdQuery, SpecialtyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSpecialtyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SpecialtyDto> Handle(GetSpecialtyByIdQuery request, CancellationToken cancellationToken)
    {
        var specialty = await _unitOfWork.Specialties.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Especialidad con Id {request.Id} no encontrada.");

        return _mapper.Map<SpecialtyDto>(specialty);
    }
}