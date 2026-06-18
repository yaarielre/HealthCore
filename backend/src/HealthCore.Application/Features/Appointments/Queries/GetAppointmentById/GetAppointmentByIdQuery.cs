using HealthCore.Application.Features.Appointments.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Queries.GetAppointmentById;
public record GetAppointmentByIdQuery(Guid Id) : IRequest<AppointmentDto>;
