using MediatR;
using HealthCore.Application.Features.Orders.DTOs;

namespace HealthCore.Application.Features.Orders.Queries.GetAll;

public record GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>;
