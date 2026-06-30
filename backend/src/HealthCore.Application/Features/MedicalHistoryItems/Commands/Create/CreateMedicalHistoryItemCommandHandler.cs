using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalHistoryItems.Commands.Create;

public class CreateMedicalHistoryItemCommandHandler : IRequestHandler<CreateMedicalHistoryItemCommand, MedicalHistoryItemDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMedicalHistoryItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalHistoryItemDto> Handle(CreateMedicalHistoryItemCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(request.Dto.PatientId)
            ?? throw new KeyNotFoundException($"Paciente con Id '{request.Dto.PatientId}' no encontrado.");

        var recordedBy = await _unitOfWork.Users.GetByIdAsync(request.Dto.RecordedById)
            ?? throw new KeyNotFoundException($"Usuario registrador con Id '{request.Dto.RecordedById}' no encontrado.");

        var item = new MedicalHistoryItem
        {
            Id = Guid.NewGuid(),
            PatientId = request.Dto.PatientId,
            Category = request.Dto.Category,
            Description = request.Dto.Description,
            Details = request.Dto.Details,
            RecordedDate = request.Dto.RecordedDate,
            Severity = request.Dto.Severity,
            RecordedById = request.Dto.RecordedById
        };

        await _unitOfWork.MedicalHistoryItems.AddAsync(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MedicalHistoryItemDto>(item) with
        {
            PatientName = $"{patient.FirstName} {patient.LastName}",
            RecordedByName = $"{recordedBy.FirstName} {recordedBy.LastName}"
        };
    }
}
