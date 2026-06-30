using HealthCore.Application.Features.InvoiceItems.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InvoiceItems.Commands.Update;

public record UpdateInvoiceItemCommand(
    Guid Id,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal DiscountPercent,
    decimal TotalPrice) : IRequest<InvoiceItemDto>;
