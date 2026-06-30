using MediatR;
using HealthCore.Application.Features.OrderTypes.DTOs;

namespace HealthCore.Application.Features.OrderTypes.Queries.GetAll;

public record GetAllOrderTypesQuery : IRequest<IEnumerable<OrderTypeDto>>;
