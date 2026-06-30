namespace HealthCore.Application.Features.OrderTypes.DTOs;

public record OrderTypeDto(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive,
    DateTime CreatedAt
);

public record CreateOrderTypeDto(
    string Name,
    string? Description
);

public record UpdateOrderTypeDto(
    string Name,
    string? Description,
    bool IsActive
);
