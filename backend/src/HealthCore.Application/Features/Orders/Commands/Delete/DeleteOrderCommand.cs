using MediatR;

namespace HealthCore.Application.Features.Orders.Commands.Delete;

public record DeleteOrderCommand(Guid Id) : IRequest<bool>;
