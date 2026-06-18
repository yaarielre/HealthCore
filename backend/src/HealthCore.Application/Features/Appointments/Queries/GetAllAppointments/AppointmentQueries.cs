using HealthCore.Application.Features.Appointments.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Queries.GetAllAppointments;
public record GetAllAppointmentsQuery() : IRequest<IEnumerable<AppointmentDto>>;