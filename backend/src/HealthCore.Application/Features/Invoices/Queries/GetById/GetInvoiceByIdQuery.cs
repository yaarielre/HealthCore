using HealthCore.Application.Features.Invoices.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Invoices.Queries.GetById;

public record GetInvoiceByIdQuery(Guid Id) : IRequest<InvoiceDto>;
