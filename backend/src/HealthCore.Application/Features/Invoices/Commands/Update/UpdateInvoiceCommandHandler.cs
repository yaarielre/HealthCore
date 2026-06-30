using AutoMapper;
using HealthCore.Application.Features.Invoices.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Invoices.Commands.Update;

public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, InvoiceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Invoices.GetWithItemsAsync(request.Id)
            ?? throw new KeyNotFoundException($"Factura con Id {request.Id} no encontrada.");

        if (invoice.Status == Domain.Enums.InvoiceStatus.Cancelled)
            throw new ApplicationException("No se puede modificar una factura cancelada.");

        invoice.DueDate = request.DueDate;
        invoice.SubTotal = request.SubTotal;
        invoice.TaxAmount = request.TaxAmount;
        invoice.DiscountAmount = request.DiscountAmount;
        invoice.TotalAmount = request.TotalAmount;
        invoice.BalanceDue = request.TotalAmount - invoice.PaidAmount;
        invoice.Notes = request.Notes;

        await _unitOfWork.Invoices.UpdateAsync(invoice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InvoiceDto>(invoice);
    }
}
