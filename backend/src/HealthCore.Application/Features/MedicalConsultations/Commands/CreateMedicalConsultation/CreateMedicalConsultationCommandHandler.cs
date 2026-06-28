using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalConsultations.Commands.CreateMedicalConsultation;

public class CreateMedicalConsultationCommandHandler : IRequestHandler<CreateMedicalConsultationCommand, MedicalConsultationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMedicalConsultationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalConsultationDto> Handle(CreateMedicalConsultationCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Dto.DoctorId)
            ?? throw new KeyNotFoundException($"Doctor con Id '{request.Dto.DoctorId}' no encontrado.");

        var patient = await _unitOfWork.Patients.GetByIdAsync(request.Dto.PatientId)
            ?? throw new KeyNotFoundException($"Paciente con Id '{request.Dto.PatientId}' no encontrado.");

        var consultation = new MedicalConsultation
        {
            Id = Guid.NewGuid(),
            PatientId = request.Dto.PatientId,
            DoctorId = request.Dto.DoctorId,
            AppointmentId = request.Dto.AppointmentId,
            ReasonForVisit = request.Dto.ReasonForVisit,
            Symptoms = request.Dto.Symptoms,
            Diagnosis = request.Dto.Diagnosis,
            Treatment = request.Dto.Treatment,
            Observations = request.Dto.Observations,
            InternalNotes = request.Dto.InternalNotes
        };

        if (request.Dto.VitalSigns is not null)
        {
            consultation.VitalSigns.Add(new VitalSign
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

        await _unitOfWork.MedicalConsultations.AddAsync(consultation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MedicalConsultationDto>(consultation) with
        {
            PatientName = $"{patient.FirstName} {patient.LastName}",
            DoctorName = $"{doctor.FirstName} {doctor.LastName}"
        };
    }
}
