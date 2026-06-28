using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;

namespace HealthCore.Application.Features.MedicalConsultations.Commands.CreateMedicalConsultation;

public record CreateMedicalConsultationCommand(CreateMedicalConsultationDto Dto) : IRequest<MedicalConsultationDto>;
