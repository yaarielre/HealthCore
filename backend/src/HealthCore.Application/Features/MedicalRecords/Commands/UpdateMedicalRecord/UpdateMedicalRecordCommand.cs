using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;

namespace HealthCore.Application.Features.MedicalRecords.Commands.UpdateMedicalRecord;

public record UpdateMedicalRecordCommand(Guid Id, UpdateMedicalRecordDto Dto) : IRequest<MedicalRecordDto>;
