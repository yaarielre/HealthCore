using HealthCore.Domain.Enums;

namespace HealthCore.Application.Features.MedicalHistoryItems.DTOs;

public record MedicalHistoryItemDto(
    Guid Id,
    Guid PatientId,
    string PatientName,
    MedicalHistoryCategory Category,
    string Description,
    string? Details,
    DateTime? RecordedDate,
    int? Severity,
    bool IsActive,
    Guid RecordedById,
    string RecordedByName,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateMedicalHistoryItemDto(
    Guid PatientId,
    MedicalHistoryCategory Category,
    string Description,
    string? Details,
    DateTime? RecordedDate,
    int? Severity,
    Guid RecordedById
);

public record UpdateMedicalHistoryItemDto(
    MedicalHistoryCategory Category,
    string Description,
    string? Details,
    DateTime? RecordedDate,
    int? Severity,
    bool IsActive
);
