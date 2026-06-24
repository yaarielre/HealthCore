using HealthCore.Application.Features.Appointments.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Queries.GetAppointmentsByDate;
public record GetAppointmentsByDateQuery(DateTime Date) : IRequest<IEnumerable<AppointmentDto>>;