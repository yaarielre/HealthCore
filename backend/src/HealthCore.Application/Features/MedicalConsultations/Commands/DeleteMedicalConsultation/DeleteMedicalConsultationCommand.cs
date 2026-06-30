using MediatR;

namespace HealthCore.Application.Features.MedicalConsultations.Commands.DeleteMedicalConsultation;

public record DeleteMedicalConsultationCommand(Guid Id) : IRequest<bool>;
