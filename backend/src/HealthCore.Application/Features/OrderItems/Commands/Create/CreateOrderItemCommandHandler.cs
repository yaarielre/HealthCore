using AutoMapper;
using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.OrderItems.Commands.Create;

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, OrderItemDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderItemDto> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId)
            ?? throw new KeyNotFoundException($"Orden con Id '{request.OrderId}' no encontrada.");

        var item = new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = request.OrderId,
            ItemName = request.Dto.ItemName,
            Description = request.Dto.Description,
            Quantity = request.Dto.Quantity,
            UnitPrice = request.Dto.UnitPrice
        };

        await _unitOfWork.OrderItems.AddAsync(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderItemDto>(item) with
        {
            OrderNumber = order.OrderNumber,
            TotalPrice = item.UnitPrice.HasValue ? item.UnitPrice * item.Quantity : null
        };
    }
}
