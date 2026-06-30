using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;

namespace HealthCore.Application.Features.OrderItems.Queries.GetByOrder;

public record GetOrderItemsByOrderQuery(Guid OrderId) : IRequest<IEnumerable<OrderItemDto>>;
