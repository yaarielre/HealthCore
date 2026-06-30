using HealthCore.Application.Features.Invoices.DTOs;
using HealthCore.Application.Features.InvoiceItems.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Invoices.Commands.Create;

public record CreateInvoiceCommand(
    Guid PatientId,
    Guid IssuedById,
    DateTime DueDate,
    decimal SubTotal,
    decimal TaxAmount,
    decimal DiscountAmount,
    decimal TotalAmount,
    string? Notes,
    List<CreateInvoiceItemDto> Items) : IRequest<InvoiceDto>;
