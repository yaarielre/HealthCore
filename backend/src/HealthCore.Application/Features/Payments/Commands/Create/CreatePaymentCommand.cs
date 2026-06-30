using HealthCore.Application.Features.Payments.DTOs;
using HealthCore.Domain.Enums;
using MediatR;

namespace HealthCore.Application.Features.Payments.Commands.Create;

public record CreatePaymentCommand(
    Guid InvoiceId,
    decimal Amount,
    PaymentMethod PaymentMethod,
    string? ReferenceNumber,
    Guid ReceivedById,
    string? Notes) : IRequest<PaymentDto>;
