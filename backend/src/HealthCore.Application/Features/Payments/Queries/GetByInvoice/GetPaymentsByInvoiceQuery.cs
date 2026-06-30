using HealthCore.Application.Features.Payments.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Payments.Queries.GetByInvoice;

public record GetPaymentsByInvoiceQuery(Guid InvoiceId) : IRequest<IEnumerable<PaymentDto>>;
