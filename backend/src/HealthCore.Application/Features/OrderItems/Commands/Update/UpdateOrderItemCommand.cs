using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;

namespace HealthCore.Application.Features.OrderItems.Commands.Update;

public record UpdateOrderItemCommand(Guid Id, UpdateOrderItemDto Dto) : IRequest<OrderItemDto>;
