using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;

namespace HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationsByPatient;

public record GetMedicalConsultationsByPatientQuery(Guid PatientId) : IRequest<IEnumerable<MedicalConsultationDto>>;
