using AutoMapper;
using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.OrderItems.Queries.GetByOrder;

public class GetOrderItemsByOrderQueryHandler : IRequestHandler<GetOrderItemsByOrderQuery, IEnumerable<OrderItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderItemsByOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderItemDto>> Handle(GetOrderItemsByOrderQuery request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.OrderItems.GetByOrderIdAsync(request.OrderId);
        return _mapper.Map<IEnumerable<OrderItemDto>>(items);
    }
}
