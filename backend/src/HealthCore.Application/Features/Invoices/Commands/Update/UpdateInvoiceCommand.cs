using HealthCore.Application.Features.Invoices.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Invoices.Commands.Update;

public record UpdateInvoiceCommand(
    Guid Id,
    DateTime DueDate,
    decimal SubTotal,
    decimal TaxAmount,
    decimal DiscountAmount,
    decimal TotalAmount,
    string? Notes) : IRequest<InvoiceDto>;
