using MediatR;
using HealthCore.Application.Features.Orders.DTOs;

namespace HealthCore.Application.Features.Orders.Commands.Create;

public record CreateOrderCommand(CreateOrderDto Dto) : IRequest<OrderDto>;
