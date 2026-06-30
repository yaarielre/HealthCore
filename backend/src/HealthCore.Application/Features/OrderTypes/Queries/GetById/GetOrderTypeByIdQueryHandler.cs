using AutoMapper;
using MediatR;
using HealthCore.Application.Features.OrderTypes.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.OrderTypes.Queries.GetById;

public class GetOrderTypeByIdQueryHandler : IRequestHandler<GetOrderTypeByIdQuery, OrderTypeDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderTypeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderTypeDto?> Handle(GetOrderTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var orderType = await _unitOfWork.OrderTypes.GetByIdAsync(request.Id);
        return orderType is null ? null : _mapper.Map<OrderTypeDto>(orderType);
    }
}
