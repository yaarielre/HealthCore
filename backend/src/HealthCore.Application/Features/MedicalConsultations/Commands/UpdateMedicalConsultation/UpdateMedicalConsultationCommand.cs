using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;

namespace HealthCore.Application.Features.MedicalConsultations.Commands.UpdateMedicalConsultation;

public record UpdateMedicalConsultationCommand(Guid Id, UpdateMedicalConsultationDto Dto) : IRequest<MedicalConsultationDto>;
