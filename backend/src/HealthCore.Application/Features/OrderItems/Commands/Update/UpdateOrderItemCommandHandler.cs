using AutoMapper;
using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.OrderItems.Commands.Update;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, OrderItemDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderItemDto> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.OrderItems.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Item de orden con Id '{request.Id}' no encontrado.");

        _mapper.Map(request.Dto, item);
        item.UpdatedAt = DateTime.UtcNow;

        if (request.Dto.IsCompleted)
            item.ResultedAt = DateTime.UtcNow;

        await _unitOfWork.OrderItems.UpdateAsync(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderItemDto>(item);
    }
}
