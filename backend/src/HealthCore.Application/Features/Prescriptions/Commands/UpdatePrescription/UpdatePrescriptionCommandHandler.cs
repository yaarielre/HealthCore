using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Prescriptions.Commands.UpdatePrescription;

public class UpdatePrescriptionCommandHandler : IRequestHandler<UpdatePrescriptionCommand, PrescriptionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePrescriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PrescriptionDto> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Receta con Id '{request.Id}' no encontrada.");

        _mapper.Map(request.Dto, prescription);
        prescription.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Prescriptions.UpdateAsync(prescription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PrescriptionDto>(prescription);
    }
}
