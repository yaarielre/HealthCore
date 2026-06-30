using MediatR;
using HealthCore.Application.Features.OrderTypes.DTOs;

namespace HealthCore.Application.Features.OrderTypes.Queries.GetById;

public record GetOrderTypeByIdQuery(Guid Id) : IRequest<OrderTypeDto?>;
