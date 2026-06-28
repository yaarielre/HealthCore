using HealthCore.Domain.Enums;

namespace HealthCore.Application.Features.Patients.DTOs;

public class PatientMedicalHistoryDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string IdNumber { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Email { get; set; }
    public GenderType? Gender { get; set; }
    public BloodType? BloodType { get; set; }
    public string? Allergies { get; set; }
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhone { get; set; }
    public List<MedicalHistoryAppointmentDto> Appointments { get; set; } = new();
    public List<MedicalHistoryConsultationDto> Consultations { get; set; } = new();
    public List<MedicalHistoryPrescriptionDto> Prescriptions { get; set; } = new();
}

public class MedicalHistoryAppointmentDto
{
    public Guid Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string Status { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
}

public class MedicalHistoryConsultationDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string ReasonForVisit { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
    public string? Treatment { get; set; }
    public string DoctorName { get; set; } = string.Empty;
}

public class MedicalHistoryPrescriptionDto
{
    public Guid Id { get; set; }
    public string Medication { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public string DoctorName { get; set; } = string.Empty;
}
