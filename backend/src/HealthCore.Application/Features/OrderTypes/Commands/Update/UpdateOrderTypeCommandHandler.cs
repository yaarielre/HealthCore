using AutoMapper;
using MediatR;
using HealthCore.Application.Features.OrderTypes.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.OrderTypes.Commands.Update;

public class UpdateOrderTypeCommandHandler : IRequestHandler<UpdateOrderTypeCommand, OrderTypeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderTypeDto> Handle(UpdateOrderTypeCommand request, CancellationToken cancellationToken)
    {
        var orderType = await _unitOfWork.OrderTypes.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Tipo de orden con Id '{request.Id}' no encontrado.");

        _mapper.Map(request.Dto, orderType);
        orderType.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.OrderTypes.UpdateAsync(orderType);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderTypeDto>(orderType);
    }
}
