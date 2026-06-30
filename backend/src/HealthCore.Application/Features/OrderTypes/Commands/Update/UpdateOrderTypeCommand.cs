using MediatR;
using HealthCore.Application.Features.OrderTypes.DTOs;

namespace HealthCore.Application.Features.OrderTypes.Commands.Update;

public record UpdateOrderTypeCommand(Guid Id, UpdateOrderTypeDto Dto) : IRequest<OrderTypeDto>;
