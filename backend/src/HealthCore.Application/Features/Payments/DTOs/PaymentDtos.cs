using HealthCore.Domain.Enums;

namespace HealthCore.Application.Features.Payments.DTOs;

public class PaymentDto
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public string? InvoiceNumber { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? ReferenceNumber { get; set; }
    public Guid ReceivedById { get; set; }
    public string? ReceivedByName { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreatePaymentDto
{
    public Guid InvoiceId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? ReferenceNumber { get; set; }
    public Guid ReceivedById { get; set; }
    public string? Notes { get; set; }
}
