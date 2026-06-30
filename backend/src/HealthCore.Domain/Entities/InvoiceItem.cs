using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;

public class InvoiceItem : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid? OrderItemId { get; set; }

    public Invoice Invoice { get; set; } = null!;
    public OrderItem? OrderItem { get; set; }
}
