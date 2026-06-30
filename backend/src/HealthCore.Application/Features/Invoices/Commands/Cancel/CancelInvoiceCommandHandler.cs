using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Invoices.Commands.Cancel;

public class CancelInvoiceCommandHandler : IRequestHandler<CancelInvoiceCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public CancelInvoiceCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(CancelInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Invoices.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Factura con Id {request.Id} no encontrada.");

        if (invoice.Status == Domain.Enums.InvoiceStatus.Cancelled)
            throw new ApplicationException("La factura ya está cancelada.");

        if (invoice.PaidAmount > 0)
            throw new ApplicationException("No se puede cancelar una factura con pagos registrados.");

        invoice.Status = Domain.Enums.InvoiceStatus.Cancelled;
        invoice.CancelledAt = DateTime.UtcNow;

        await _unitOfWork.Invoices.UpdateAsync(invoice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
