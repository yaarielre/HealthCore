using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Immunizations.Commands.Update;

public class UpdateImmunizationCommandHandler : IRequestHandler<UpdateImmunizationCommand, ImmunizationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateImmunizationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ImmunizationDto> Handle(UpdateImmunizationCommand request, CancellationToken cancellationToken)
    {
        var immunization = await _unitOfWork.Immunizations.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Inmunización con Id '{request.Id}' no encontrada.");

        _mapper.Map(request.Dto, immunization);
        immunization.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Immunizations.UpdateAsync(immunization);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ImmunizationDto>(immunization);
    }
}
