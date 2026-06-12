using HealthCore.Domain.enums;
using HealthCore.Domain.Enums;

namespace HealthCore.Application.Features.Users.DTOs;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string IdNumber,
    string Email,
    string Phone,
    UserRole Role,
    AccountStatus Status,
    Guid? DoctorId
);

public record CreateUserDto(
    string FirstName,
    string LastName,
    string IdNumber,
    string Email,
    string Phone,
    UserRole Role,
    Guid? DoctorId
);

public record UpdateUserDto(
    string FirstName,
    string LastName,
    string Phone,
    UserRole Role,
    Guid? DoctorId
);