using MediatR;

namespace HealthCore.Application.Features.InvoiceItems.Commands.Delete;

public record DeleteInvoiceItemCommand(Guid Id) : IRequest<bool>;
