using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;

namespace HealthCore.Application.Features.OrderItems.Commands.Create;

public record CreateOrderItemCommand(Guid OrderId, CreateOrderItemDto Dto) : IRequest<OrderItemDto>;
