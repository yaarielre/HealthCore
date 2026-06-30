using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalRecords.Commands.UpdateMedicalRecord;

public class UpdateMedicalRecordCommandHandler : IRequestHandler<UpdateMedicalRecordCommand, MedicalRecordDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMedicalRecordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalRecordDto> Handle(UpdateMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        var record = await _unitOfWork.MedicalRecords.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Historial clínico con Id '{request.Id}' no encontrado.");

        _mapper.Map(request.Dto, record);
        record.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.MedicalRecords.UpdateAsync(record);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MedicalRecordDto>(record);
    }
}
