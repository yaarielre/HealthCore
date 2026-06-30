namespace HealthCore.Application.Features.Immunizations.DTOs;

public record ImmunizationDto(
    Guid Id,
    Guid PatientId,
    string PatientName,
    string VaccineName,
    int DoseNumber,
    DateTime ApplicationDate,
    DateTime? NextDoseDate,
    string? BatchNumber,
    string? AdministeredBy,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateImmunizationDto(
    Guid PatientId,
    string VaccineName,
    int DoseNumber,
    DateTime ApplicationDate,
    DateTime? NextDoseDate,
    string? BatchNumber,
    string? AdministeredBy,
    string? Notes
);

public record UpdateImmunizationDto(
    string VaccineName,
    int DoseNumber,
    DateTime ApplicationDate,
    DateTime? NextDoseDate,
    string? BatchNumber,
    string? AdministeredBy,
    string? Notes
);
