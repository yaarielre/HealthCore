using MediatR;

namespace HealthCore.Application.Features.OrderItems.Commands.Delete;

public record DeleteOrderItemCommand(Guid Id) : IRequest<bool>;
