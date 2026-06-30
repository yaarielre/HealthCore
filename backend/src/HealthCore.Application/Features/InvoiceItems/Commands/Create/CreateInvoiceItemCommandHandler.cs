using AutoMapper;
using HealthCore.Application.Features.InvoiceItems.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InvoiceItems.Commands.Create;

public class CreateInvoiceItemCommandHandler : IRequestHandler<CreateInvoiceItemCommand, InvoiceItemDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateInvoiceItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceItemDto> Handle(CreateInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Invoices.GetByIdAsync(request.InvoiceId)
            ?? throw new KeyNotFoundException($"Factura con Id {request.InvoiceId} no encontrada.");

        if (invoice.Status == Domain.Enums.InvoiceStatus.Cancelled)
            throw new ApplicationException("No se pueden agregar items a una factura cancelada.");

        var item = new InvoiceItem
        {
            InvoiceId = request.InvoiceId,
            Description = request.Description,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            DiscountPercent = request.DiscountPercent,
            TotalPrice = request.TotalPrice,
            OrderItemId = request.OrderItemId
        };

        await _unitOfWork.InvoiceItems.AddAsync(item);

        // Recalcular totales de la factura
        var items = await _unitOfWork.InvoiceItems.GetByInvoiceIdAsync(request.InvoiceId);
        invoice.SubTotal = items.Sum(i => i.UnitPrice * i.Quantity);
        invoice.TotalAmount = items.Sum(i => i.TotalPrice);
        invoice.BalanceDue = invoice.TotalAmount - invoice.PaidAmount;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InvoiceItemDto>(item);
    }
}
