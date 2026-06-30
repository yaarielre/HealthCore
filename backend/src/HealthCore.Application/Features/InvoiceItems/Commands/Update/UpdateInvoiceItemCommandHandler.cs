using AutoMapper;
using HealthCore.Application.Features.InvoiceItems.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InvoiceItems.Commands.Update;

public class UpdateInvoiceItemCommandHandler : IRequestHandler<UpdateInvoiceItemCommand, InvoiceItemDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateInvoiceItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceItemDto> Handle(UpdateInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.InvoiceItems.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Item de factura con Id {request.Id} no encontrado.");

        var invoice = await _unitOfWork.Invoices.GetByIdAsync(item.InvoiceId);
        if (invoice?.Status == Domain.Enums.InvoiceStatus.Cancelled)
            throw new ApplicationException("No se pueden modificar items de una factura cancelada.");

        item.Description = request.Description;
        item.Quantity = request.Quantity;
        item.UnitPrice = request.UnitPrice;
        item.DiscountPercent = request.DiscountPercent;
        item.TotalPrice = request.TotalPrice;

        await _unitOfWork.InvoiceItems.UpdateAsync(item);

        if (invoice != null)
        {
            var items = await _unitOfWork.InvoiceItems.GetByInvoiceIdAsync(item.InvoiceId);
            invoice.SubTotal = items.Sum(i => i.UnitPrice * i.Quantity);
            invoice.TotalAmount = items.Sum(i => i.TotalPrice);
            invoice.BalanceDue = invoice.TotalAmount - invoice.PaidAmount;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InvoiceItemDto>(item);
    }
}
