using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;

namespace HealthCore.Application.Features.MedicalHistoryItems.Queries.GetByPatient;

public record GetMedicalHistoryItemsByPatientQuery(Guid PatientId) : IRequest<IEnumerable<MedicalHistoryItemDto>>;
