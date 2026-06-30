namespace HealthCore.Application.Features.OrderItems.DTOs;

public record OrderItemDto(
    Guid Id,
    Guid OrderId,
    string OrderNumber,
    string ItemName,
    string? Description,
    int Quantity,
    decimal? UnitPrice,
    decimal? TotalPrice,
    string? Results,
    string? ResultUrl,
    DateTime? ResultedAt,
    Guid? ResultedBy,
    bool IsCompleted,
    DateTime CreatedAt
);

public record CreateOrderItemDto(
    string ItemName,
    string? Description,
    int Quantity,
    decimal? UnitPrice
);

public record UpdateOrderItemDto(
    string ItemName,
    string? Description,
    int Quantity,
    decimal? UnitPrice,
    string? Results,
    string? ResultUrl,
    Guid? ResultedBy,
    bool IsCompleted
);
