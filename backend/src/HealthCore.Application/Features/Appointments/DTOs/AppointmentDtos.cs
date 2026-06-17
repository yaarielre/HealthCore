using HealthCore.Domain.Enums;

namespace HealthCore.Application.Features.Appointments.DTOs;
public record AppointmentDto(
    Guid Id,
    Guid PatientId,
    string PatientName,
    Guid DoctorId,
    string DoctorName,
    DateTime AppointmentDate,
    string Reason,
    string? Notes,
    AppointmentStatus Status
);

public record CreateAppointmentDto(
    Guid PatientId,
    Guid DoctorId,
    DateTime AppointmentDate,
    string Reason,
    string? Notes

);

public record UpdateAppointmentDto(
    DateTime AppointmentDate,
    string Reason,
    string? Notes
);