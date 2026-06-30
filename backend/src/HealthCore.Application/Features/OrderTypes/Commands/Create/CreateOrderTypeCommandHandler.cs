using AutoMapper;
using MediatR;
using HealthCore.Application.Features.OrderTypes.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.OrderTypes.Commands.Create;

public class CreateOrderTypeCommandHandler : IRequestHandler<CreateOrderTypeCommand, OrderTypeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderTypeDto> Handle(CreateOrderTypeCommand request, CancellationToken cancellationToken)
    {
        var orderType = new OrderType
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            Description = request.Dto.Description
        };

        await _unitOfWork.OrderTypes.AddAsync(orderType);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderTypeDto>(orderType);
    }
}
