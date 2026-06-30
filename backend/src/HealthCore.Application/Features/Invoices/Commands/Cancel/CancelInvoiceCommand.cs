using MediatR;

namespace HealthCore.Application.Features.Invoices.Commands.Cancel;

public record CancelInvoiceCommand(Guid Id) : IRequest<bool>;
