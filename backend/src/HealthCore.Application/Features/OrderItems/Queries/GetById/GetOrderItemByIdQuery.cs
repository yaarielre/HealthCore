using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;

namespace HealthCore.Application.Features.OrderItems.Queries.GetById;

public record GetOrderItemByIdQuery(Guid Id) : IRequest<OrderItemDto?>;
