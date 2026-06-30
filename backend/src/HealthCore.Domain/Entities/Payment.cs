using HealthCore.Domain.Common;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? ReferenceNumber { get; set; }
    public Guid ReceivedById { get; set; }
    public string? Notes { get; set; }

    public Invoice Invoice { get; set; } = null!;
    public User ReceivedBy { get; set; } = null!;
}
