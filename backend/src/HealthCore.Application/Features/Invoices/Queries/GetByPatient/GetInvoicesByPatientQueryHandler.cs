using AutoMapper;
using HealthCore.Application.Features.Invoices.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Invoices.Queries.GetByPatient;

public class GetInvoicesByPatientQueryHandler : IRequestHandler<GetInvoicesByPatientQuery, IEnumerable<InvoiceDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetInvoicesByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InvoiceDto>> Handle(GetInvoicesByPatientQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _unitOfWork.Invoices.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
    }
}
