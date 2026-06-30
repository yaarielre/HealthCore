using AutoMapper;
using HealthCore.Application.Features.Invoices.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Invoices.Commands.Create;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(request.PatientId)
            ?? throw new KeyNotFoundException($"Paciente con Id {request.PatientId} no encontrado.");

        var invoiceNumber = await _unitOfWork.Invoices.GenerateInvoiceNumberAsync();
        var totalAmount = request.TotalAmount;

        var invoice = new Invoice
        {
            InvoiceNumber = invoiceNumber,
            PatientId = request.PatientId,
            IssuedById = request.IssuedById,
            InvoiceDate = DateTime.UtcNow,
            DueDate = request.DueDate,
            SubTotal = request.SubTotal,
            TaxAmount = request.TaxAmount,
            DiscountAmount = request.DiscountAmount,
            TotalAmount = totalAmount,
            PaidAmount = 0,
            BalanceDue = totalAmount,
            Status = Domain.Enums.InvoiceStatus.Unpaid,
            Notes = request.Notes
        };

        foreach (var itemDto in request.Items)
        {
            invoice.InvoiceItems.Add(new InvoiceItem
            {
                Description = itemDto.Description,
                Quantity = itemDto.Quantity,
                UnitPrice = itemDto.UnitPrice,
                DiscountPercent = itemDto.DiscountPercent,
                TotalPrice = itemDto.TotalPrice,
                OrderItemId = itemDto.OrderItemId
            });
        }

        await _unitOfWork.Invoices.AddAsync(invoice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var created = await _unitOfWork.Invoices.GetWithItemsAsync(invoice.Id);
        return _mapper.Map<InvoiceDto>(created);
    }
}
