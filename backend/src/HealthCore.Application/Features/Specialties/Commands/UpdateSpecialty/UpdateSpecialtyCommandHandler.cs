using AutoMapper;
using HealthCore.Application.Features.Specialties.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Specialties.Commands.UpdateSpecialty;

public class UpdateSpecialtyCommandHandler : IRequestHandler<UpdateSpecialtyCommand, SpecialtyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSpecialtyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SpecialtyDto> Handle(UpdateSpecialtyCommand request, CancellationToken cancellationToken)
    {
        var specialty = await _unitOfWork.Specialties.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Especialidad con Id {request.Id} no encontrada.");

        if (await _unitOfWork.Specialties.ExistsByNameAsync(request.Name, request.Id))
            throw new ApplicationException($"Ya existe otra especialidad con el nombre '{request.Name}'.");

        specialty.Name = request.Name;
        specialty.Description = request.Description;

        await _unitOfWork.Specialties.UpdateAsync(specialty);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SpecialtyDto>(specialty);
    }
}