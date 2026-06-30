using AutoMapper;
using MediatR;
using HealthCore.Application.Features.OrderTypes.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.OrderTypes.Queries.GetAll;

public class GetAllOrderTypesQueryHandler : IRequestHandler<GetAllOrderTypesQuery, IEnumerable<OrderTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllOrderTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderTypeDto>> Handle(GetAllOrderTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await _unitOfWork.OrderTypes.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderTypeDto>>(types);
    }
}
