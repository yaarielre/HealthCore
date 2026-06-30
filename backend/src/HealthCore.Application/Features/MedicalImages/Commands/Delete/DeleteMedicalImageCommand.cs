using MediatR;

namespace HealthCore.Application.Features.MedicalImages.Commands.Delete;

public record DeleteMedicalImageCommand(Guid Id) : IRequest<bool>;
