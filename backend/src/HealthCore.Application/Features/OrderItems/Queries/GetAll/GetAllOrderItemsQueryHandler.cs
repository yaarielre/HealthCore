using AutoMapper;
using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.OrderItems.Queries.GetAll;

public class GetAllOrderItemsQueryHandler : IRequestHandler<GetAllOrderItemsQuery, IEnumerable<OrderItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllOrderItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderItemDto>> Handle(GetAllOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.OrderItems.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderItemDto>>(items);
    }
}
