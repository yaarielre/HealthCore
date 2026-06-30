using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalConsultations.Commands.UpdateMedicalConsultation;

public class UpdateMedicalConsultationCommandHandler : IRequestHandler<UpdateMedicalConsultationCommand, MedicalConsultationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMedicalConsultationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalConsultationDto> Handle(UpdateMedicalConsultationCommand request, CancellationToken cancellationToken)
    {
        var consultation = await _unitOfWork.MedicalConsultations.GetWithDetailsAsync(request.Id)
            ?? throw new KeyNotFoundException($"Consulta con Id '{request.Id}' no encontrada.");

        _mapper.Map(request.Dto, consultation);
        consultation.UpdatedAt = DateTime.UtcNow;

        if (request.Dto.VitalSigns is not null)
        {
            consultation.VitalSigns.Clear();
            consultation.VitalSigns.Add(new Domain.Entities.VitalSign
            {
                Id = Guid.NewGuid(),
                MedicalConsultationId = consultation.Id,
                BloodPressure = request.Dto.VitalSigns.BloodPressure,
                Temperature = request.Dto.VitalSigns.Temperature,
                Weight = request.Dto.VitalSigns.Weight,
                Height = request.Dto.VitalSigns.Height,
                HeartRate = request.Dto.VitalSigns.HeartRate,
                OxygenSaturation = request.Dto.VitalSigns.OxygenSaturation
            });
        }

        await _unitOfWork.MedicalConsultations.UpdateAsync(consultation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MedicalConsultationDto>(consultation);
    }
}
