using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Orders.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Orders.Queries.GetById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetWithItemsAsync(request.Id);
        return order is null ? null : _mapper.Map<OrderDto>(order);
    }
}
