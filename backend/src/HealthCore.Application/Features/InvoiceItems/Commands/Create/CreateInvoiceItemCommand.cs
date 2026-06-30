using HealthCore.Application.Features.InvoiceItems.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InvoiceItems.Commands.Create;

public record CreateInvoiceItemCommand(
    Guid InvoiceId,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal DiscountPercent,
    decimal TotalPrice,
    Guid? OrderItemId) : IRequest<InvoiceItemDto>;
