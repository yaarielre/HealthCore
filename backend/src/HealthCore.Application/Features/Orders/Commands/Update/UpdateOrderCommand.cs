using MediatR;
using HealthCore.Application.Features.Orders.DTOs;

namespace HealthCore.Application.Features.Orders.Commands.Update;

public record UpdateOrderCommand(Guid Id, UpdateOrderDto Dto) : IRequest<OrderDto>;
