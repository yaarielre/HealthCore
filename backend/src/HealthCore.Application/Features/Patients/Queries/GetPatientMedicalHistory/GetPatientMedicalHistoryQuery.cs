using MediatR;
using HealthCore.Application.Features.Patients.DTOs;

namespace HealthCore.Application.Features.Patients.Queries.GetPatientMedicalHistory;

public record GetPatientMedicalHistoryQuery(Guid PatientId) : IRequest<PatientMedicalHistoryDto>;
