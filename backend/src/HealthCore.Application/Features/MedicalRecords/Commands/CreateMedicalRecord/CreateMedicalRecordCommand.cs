using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;

namespace HealthCore.Application.Features.MedicalRecords.Commands.CreateMedicalRecord;

public record CreateMedicalRecordCommand(CreateMedicalRecordDto Dto) : IRequest<MedicalRecordDto>;
