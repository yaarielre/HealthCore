using HealthCore.Application.Features.Appointments.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Appointment.Queries.GetAppointmentsByDoctor;
public record GetAppointmentsByDoctorQuery(Guid DoctorId) : IRequest<IEnumerable<AppointmentDto>>;