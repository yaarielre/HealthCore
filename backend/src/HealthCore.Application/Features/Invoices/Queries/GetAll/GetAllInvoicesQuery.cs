using HealthCore.Application.Features.Invoices.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Invoices.Queries.GetAll;

public record GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>;
