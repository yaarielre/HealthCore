using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Orders.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetWithItemsAsync(request.Id)
            ?? throw new KeyNotFoundException($"Orden con Id '{request.Id}' no encontrada.");

        _mapper.Map(request.Dto, order);
        order.UpdatedAt = DateTime.UtcNow;

        if (request.Dto.Status == Domain.Enums.OrderStatus.Completed)
            order.CompletedAt = DateTime.UtcNow;
        else if (request.Dto.Status == Domain.Enums.OrderStatus.Cancelled)
            order.CancelledAt = DateTime.UtcNow;

        await _unitOfWork.Orders.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderDto>(order);
    }
}
