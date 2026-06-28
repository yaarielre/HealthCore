namespace HealthCore.Application.Features.MedicalConsultations.DTOs;

public record VitalSignDto(
    Guid? Id,
    string? BloodPressure,
    decimal? Temperature,
    decimal? Weight,
    decimal? Height,
    int? HeartRate,
    int? OxygenSaturation
);

public record MedicalConsultationDto(
    Guid Id,
    Guid PatientId,
    string PatientName,
    Guid DoctorId,
    string DoctorName,
    Guid? AppointmentId,
    string ReasonForVisit,
    string? Symptoms,
    string? Diagnosis,
    string? Treatment,
    string? Observations,
    string? InternalNotes,
    ICollection<VitalSignDto> VitalSigns,
    DateTime CreatedAt
);

public record CreateMedicalConsultationDto(
    Guid PatientId,
    Guid DoctorId,
    Guid? AppointmentId,
    string ReasonForVisit,
    string? Symptoms,
    string? Diagnosis,
    string? Treatment,
    string? Observations,
    string? InternalNotes,
    CreateVitalSignDto? VitalSigns
);

public record CreateVitalSignDto(
    string? BloodPressure,
    decimal? Temperature,
    decimal? Weight,
    decimal? Height,
    int? HeartRate,
    int? OxygenSaturation
);
