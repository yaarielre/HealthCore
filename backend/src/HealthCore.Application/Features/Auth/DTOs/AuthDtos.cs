using HealthCore.Domain.Enums;

namespace HealthCore.Application.Features.Auth.DTOs;

public record LoginDto(
    string Email,
    string Password
);

public record RegisterDto(
    string FirstName,
    string LastName,
    string IdNumber,
    string Email,
    string Password,
    string Phone,
    UserRole Role,
    Guid? DoctorId
);

public record AuthResponseDto(
    string Token,
    string FullName,
    string Email,
    UserRole Role,
    DateTime ExpiresAt
);