using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;

namespace HealthCore.Application.Features.OrderItems.Queries.GetAll;

public record GetAllOrderItemsQuery : IRequest<IEnumerable<OrderItemDto>>;
