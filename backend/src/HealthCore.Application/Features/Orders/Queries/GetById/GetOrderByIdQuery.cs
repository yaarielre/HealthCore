using MediatR;
using HealthCore.Application.Features.Orders.DTOs;

namespace HealthCore.Application.Features.Orders.Queries.GetById;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;
