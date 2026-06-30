using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;

namespace HealthCore.Application.Features.MedicalRecords.Queries.GetMedicalRecordById;

public record GetMedicalRecordByIdQuery(Guid Id) : IRequest<MedicalRecordDto?>;
