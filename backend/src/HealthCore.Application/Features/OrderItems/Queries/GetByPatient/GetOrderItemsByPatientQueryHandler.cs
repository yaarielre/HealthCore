using AutoMapper;
using MediatR;
using HealthCore.Application.Features.OrderItems.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.OrderItems.Queries.GetByPatient;

public class GetOrderItemsByPatientQueryHandler : IRequestHandler<GetOrderItemsByPatientQuery, IEnumerable<OrderItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderItemsByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderItemDto>> Handle(GetOrderItemsByPatientQuery request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.OrderItems.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<OrderItemDto>>(items);
    }
}
