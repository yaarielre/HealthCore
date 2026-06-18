using HealthCore.Application.Features.Appointments.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Appointment.Queries.GetAppointmentsByDate;
public record GetAppointmentsByDateQuery(DateTime Date) : IRequest<IEnumerable<AppointmentDto>>;