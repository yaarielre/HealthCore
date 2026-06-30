using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;

namespace HealthCore.Application.Features.MedicalRecords.Queries.GetMedicalRecordByPatient;

public record GetMedicalRecordByPatientQuery(Guid PatientId) : IRequest<MedicalRecordDto?>;
