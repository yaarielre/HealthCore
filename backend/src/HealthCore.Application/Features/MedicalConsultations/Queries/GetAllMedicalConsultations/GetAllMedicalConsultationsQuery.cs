using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;

namespace HealthCore.Application.Features.MedicalConsultations.Queries.GetAllMedicalConsultations;

public record GetAllMedicalConsultationsQuery : IRequest<IEnumerable<MedicalConsultationDto>>;
