using HealthCore.Domain.Common;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid? MedicalConsultationId { get; set; }
    public Guid OrderTypeId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public OrderPriority Priority { get; set; } = OrderPriority.Normal;
    public string? Notes { get; set; }
    public DateTime OrderedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public DateTime? CancelledAt { get; set; }

    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public MedicalConsultation? MedicalConsultation { get; set; }
    public OrderType OrderType { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
