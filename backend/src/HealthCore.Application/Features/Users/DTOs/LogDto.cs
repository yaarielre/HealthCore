namespace HealthCore.Application.Features.Users.DTOs;

public record LogDto(
    Guid Id,
    Guid UserId,
    string UserName,
    string Action,
    string Module,
    string? Details,
    string? IpAddress,
    DateTime CreatedAt
);
