using AutoMapper;
using HealthCore.Application.Features.Payments.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Payments.Queries.GetByInvoice;

public class GetPaymentsByInvoiceQueryHandler : IRequestHandler<GetPaymentsByInvoiceQuery, IEnumerable<PaymentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaymentsByInvoiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentDto>> Handle(GetPaymentsByInvoiceQuery request, CancellationToken cancellationToken)
    {
        var payments = await _unitOfWork.Payments.GetByInvoiceIdAsync(request.InvoiceId);
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }
}
