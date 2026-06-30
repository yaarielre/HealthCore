using MediatR;

namespace HealthCore.Application.Features.Prescriptions.Commands.DeletePrescription;

public record DeletePrescriptionCommand(Guid Id) : IRequest<bool>;
