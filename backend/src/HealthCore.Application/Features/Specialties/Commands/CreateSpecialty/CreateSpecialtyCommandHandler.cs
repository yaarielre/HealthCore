using AutoMapper;
using HealthCore.Application.Features.Specialties.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Specialties.Commands.CreateSpecialty;

public class CreateSpecialtyCommandHandler : IRequestHandler<CreateSpecialtyCommand, SpecialtyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSpecialtyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SpecialtyDto> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Specialties.ExistsByNameAsync(request.Name))
            throw new ApplicationException($"Ya existe una especialidad con el nombre '{request.Name}'.");

        var specialty = new Specialty
        {
            Name = request.Name,
            Description = request.Description,
            IsActive = true
        };

        await _unitOfWork.Specialties.AddAsync(specialty);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SpecialtyDto>(specialty);
    }
}