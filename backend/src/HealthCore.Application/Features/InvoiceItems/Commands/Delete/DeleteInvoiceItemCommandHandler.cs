using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InvoiceItems.Commands.Delete;

public class DeleteInvoiceItemCommandHandler : IRequestHandler<DeleteInvoiceItemCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInvoiceItemCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(DeleteInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.InvoiceItems.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Item de factura con Id {request.Id} no encontrado.");

        var invoice = await _unitOfWork.Invoices.GetByIdAsync(item.InvoiceId);
        if (invoice?.Status == Domain.Enums.InvoiceStatus.Cancelled)
            throw new ApplicationException("No se pueden eliminar items de una factura cancelada.");

        await _unitOfWork.InvoiceItems.DeleteAsync(request.Id);

        if (invoice != null)
        {
            var items = await _unitOfWork.InvoiceItems.GetByInvoiceIdAsync(item.InvoiceId);
            invoice.SubTotal = items.Sum(i => i.UnitPrice * i.Quantity);
            invoice.TotalAmount = items.Sum(i => i.TotalPrice);
            invoice.BalanceDue = invoice.TotalAmount - invoice.PaidAmount;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
