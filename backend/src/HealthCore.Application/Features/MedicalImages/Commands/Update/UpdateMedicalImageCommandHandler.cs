using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalImages.Commands.Update;

public class UpdateMedicalImageCommandHandler : IRequestHandler<UpdateMedicalImageCommand, MedicalImageDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMedicalImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalImageDto> Handle(UpdateMedicalImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _unitOfWork.MedicalImages.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Imagen médica con Id '{request.Id}' no encontrada.");

        _mapper.Map(request.Dto, image);
        image.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.MedicalImages.UpdateAsync(image);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MedicalImageDto>(image);
    }
}
