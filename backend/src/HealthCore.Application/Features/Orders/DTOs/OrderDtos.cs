using HealthCore.Domain.Enums;
using HealthCore.Application.Features.OrderItems.DTOs;

namespace HealthCore.Application.Features.Orders.DTOs;

public record OrderDto(
    Guid Id,
    string OrderNumber,
    Guid PatientId,
    string PatientName,
    Guid DoctorId,
    string DoctorName,
    Guid? MedicalConsultationId,
    Guid OrderTypeId,
    string OrderTypeName,
    OrderStatus Status,
    OrderPriority Priority,
    string? Notes,
    int ItemCount,
    DateTime OrderedAt,
    DateTime? CompletedAt,
    DateTime? CancelledAt,
    DateTime CreatedAt
);

public record CreateOrderDto(
    Guid PatientId,
    Guid DoctorId,
    Guid? MedicalConsultationId,
    Guid OrderTypeId,
    OrderPriority Priority,
    string? Notes,
    ICollection<CreateOrderItemDto> Items
);

public record UpdateOrderDto(
    Guid OrderTypeId,
    OrderPriority Priority,
    string? Notes,
    OrderStatus Status
);
