using MediatR;

namespace HealthCore.Application.Features.OrderTypes.Commands.Delete;

public record DeleteOrderTypeCommand(Guid Id) : IRequest<bool>;
