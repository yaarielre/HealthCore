using MediatR;

namespace HealthCore.Application.Features.MedicalHistoryItems.Commands.Delete;

public record DeleteMedicalHistoryItemCommand(Guid Id) : IRequest<bool>;
