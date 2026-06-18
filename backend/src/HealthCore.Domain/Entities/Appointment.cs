using HealthCore.Domain.Common;
using HealthCore.Domain.Enums;

namespace HealthCore.Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
}