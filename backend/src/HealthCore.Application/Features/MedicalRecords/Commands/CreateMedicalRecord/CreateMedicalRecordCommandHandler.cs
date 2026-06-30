using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalRecords.Commands.CreateMedicalRecord;

public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, MedicalRecordDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMedicalRecordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalRecordDto> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(request.Dto.PatientId)
            ?? throw new KeyNotFoundException($"Paciente con Id '{request.Dto.PatientId}' no encontrado.");

        var record = new MedicalRecord
        {
            Id = Guid.NewGuid(),
            PatientId = request.Dto.PatientId,
            RecordNumber = request.Dto.RecordNumber,
            BloodType = request.Dto.BloodType,
            Allergies = request.Dto.Allergies,
            EmergencyContactName = request.Dto.EmergencyContactName,
            EmergencyContactPhone = request.Dto.EmergencyContactPhone,
            Notes = request.Dto.Notes
        };

        await _unitOfWork.MedicalRecords.AddAsync(record);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MedicalRecordDto>(record) with
        {
            PatientName = $"{patient.FirstName} {patient.LastName}"
        };
    }
}
