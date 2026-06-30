using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;

namespace HealthCore.Application.Features.MedicalRecords.Queries.GetAllMedicalRecords;

public record GetAllMedicalRecordsQuery : IRequest<IEnumerable<MedicalRecordDto>>;
