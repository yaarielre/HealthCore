using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Prescriptions.Commands.CreatePrescription;

public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, PrescriptionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePrescriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PrescriptionDto> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Dto.DoctorId)
            ?? throw new KeyNotFoundException($"Doctor con Id '{request.Dto.DoctorId}' no encontrado.");

        var patient = await _unitOfWork.Patients.GetByIdAsync(request.Dto.PatientId)
            ?? throw new KeyNotFoundException($"Paciente con Id '{request.Dto.PatientId}' no encontrado.");

        var prescription = new Prescription
        {
            Id = Guid.NewGuid(),
            PatientId = request.Dto.PatientId,
            DoctorId = request.Dto.DoctorId,
            MedicalConsultationId = request.Dto.MedicalConsultationId,
            Medication = request.Dto.Medication,
            Dosage = request.Dto.Dosage,
            Frequency = request.Dto.Frequency,
            Duration = request.Dto.Duration,
            Instructions = request.Dto.Instructions
        };

        await _unitOfWork.Prescriptions.AddAsync(prescription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PrescriptionDto>(prescription) with
        {
            PatientName = $"{patient.FirstName} {patient.LastName}",
            DoctorName = $"{doctor.FirstName} {doctor.LastName}"
        };
    }
}
