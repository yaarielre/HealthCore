using HealthCore.Domain.Common;

namespace HealthCore.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal? UnitPrice { get; set; }
    public string? Results { get; set; }
    public string? ResultUrl { get; set; }
    public DateTime? ResultedAt { get; set; }
    public Guid? ResultedBy { get; set; }
    public bool IsCompleted { get; set; }

    public Order Order { get; set; } = null!;
    public User? ResultedByUser { get; set; }
}
