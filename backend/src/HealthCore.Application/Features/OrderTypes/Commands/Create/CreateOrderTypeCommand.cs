using MediatR;
using HealthCore.Application.Features.OrderTypes.DTOs;

namespace HealthCore.Application.Features.OrderTypes.Commands.Create;

public record CreateOrderTypeCommand(CreateOrderTypeDto Dto) : IRequest<OrderTypeDto>;
