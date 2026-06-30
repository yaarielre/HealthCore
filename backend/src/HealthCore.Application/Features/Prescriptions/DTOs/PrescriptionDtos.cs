namespace HealthCore.Application.Features.Prescriptions.DTOs;

public record PrescriptionDto(
    Guid Id,
    Guid PatientId,
    string PatientName,
    Guid DoctorId,
    string DoctorName,
    Guid MedicalConsultationId,
    string Medication,
    string Dosage,
    string Frequency,
    string Duration,
    string? Instructions,
    DateTime CreatedAt
);

public record CreatePrescriptionDto(
    Guid PatientId,
    Guid DoctorId,
    Guid MedicalConsultationId,
    string Medication,
    string Dosage,
    string Frequency,
    string Duration,
    string? Instructions
);

public record UpdatePrescriptionDto(
    Guid DoctorId,
    Guid MedicalConsultationId,
    string Medication,
    string Dosage,
    string Frequency,
    string Duration,
    string? Instructions
);
