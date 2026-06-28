using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;

namespace HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationById;

public record GetMedicalConsultationByIdQuery(Guid Id) : IRequest<MedicalConsultationDto?>;
