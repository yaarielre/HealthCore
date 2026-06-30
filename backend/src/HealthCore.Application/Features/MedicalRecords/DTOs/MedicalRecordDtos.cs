using HealthCore.Domain.Enums;

namespace HealthCore.Application.Features.MedicalRecords.DTOs;

public record MedicalRecordDto(
    Guid Id,
    Guid PatientId,
    string PatientName,
    string RecordNumber,
    BloodType? BloodType,
    string? Allergies,
    string? EmergencyContactName,
    string? EmergencyContactPhone,
    string? Notes,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateMedicalRecordDto(
    Guid PatientId,
    string RecordNumber,
    BloodType? BloodType,
    string? Allergies,
    string? EmergencyContactName,
    string? EmergencyContactPhone,
    string? Notes
);

public record UpdateMedicalRecordDto(
    string RecordNumber,
    BloodType? BloodType,
    string? Allergies,
    string? EmergencyContactName,
    string? EmergencyContactPhone,
    string? Notes,
    bool IsActive
);
