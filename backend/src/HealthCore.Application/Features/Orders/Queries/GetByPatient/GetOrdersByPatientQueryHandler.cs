using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Orders.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Orders.Queries.GetByPatient;

public class GetOrdersByPatientQueryHandler : IRequestHandler<GetOrdersByPatientQuery, IEnumerable<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrdersByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByPatientQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}
