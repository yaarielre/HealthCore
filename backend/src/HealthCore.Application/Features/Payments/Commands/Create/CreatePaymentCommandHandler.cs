using AutoMapper;
using HealthCore.Application.Features.Payments.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Payments.Commands.Create;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Invoices.GetByIdAsync(request.InvoiceId)
            ?? throw new KeyNotFoundException($"Factura con Id {request.InvoiceId} no encontrada.");

        if (invoice.Status == InvoiceStatus.Cancelled)
            throw new ApplicationException("No se pueden registrar pagos en una factura cancelada.");

        if (invoice.Status == InvoiceStatus.Paid)
            throw new ApplicationException("La factura ya está pagada.");

        if (request.Amount <= 0)
            throw new ApplicationException("El monto del pago debe ser mayor a cero.");

        var payment = new Payment
        {
            InvoiceId = request.InvoiceId,
            PaymentDate = DateTime.UtcNow,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod,
            ReferenceNumber = request.ReferenceNumber,
            ReceivedById = request.ReceivedById,
            Notes = request.Notes
        };

        await _unitOfWork.Payments.AddAsync(payment);

        // Actualizar montos de la factura
        var totalPaid = await _unitOfWork.Payments.GetTotalPaidByInvoiceIdAsync(request.InvoiceId);
        invoice.PaidAmount = totalPaid;
        invoice.BalanceDue = invoice.TotalAmount - totalPaid;
        invoice.Status = invoice.BalanceDue <= 0 ? InvoiceStatus.Paid
            : invoice.PaidAmount > 0 ? InvoiceStatus.Partial
            : InvoiceStatus.Unpaid;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PaymentDto>(payment);
    }
}
