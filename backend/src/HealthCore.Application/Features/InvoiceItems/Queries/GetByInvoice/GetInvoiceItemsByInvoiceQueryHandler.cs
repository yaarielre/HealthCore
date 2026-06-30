using AutoMapper;
using HealthCore.Application.Features.InvoiceItems.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InvoiceItems.Queries.GetByInvoice;

public class GetInvoiceItemsByInvoiceQueryHandler : IRequestHandler<GetInvoiceItemsByInvoiceQuery, IEnumerable<InvoiceItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetInvoiceItemsByInvoiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InvoiceItemDto>> Handle(GetInvoiceItemsByInvoiceQuery request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.InvoiceItems.GetByInvoiceIdAsync(request.InvoiceId);
        return _mapper.Map<IEnumerable<InvoiceItemDto>>(items);
    }
}
