using HealthCore.Application.Features.InvoiceItems.DTOs;
using MediatR;

namespace HealthCore.Application.Features.InvoiceItems.Queries.GetByInvoice;

public record GetInvoiceItemsByInvoiceQuery(Guid InvoiceId) : IRequest<IEnumerable<InvoiceItemDto>>;
