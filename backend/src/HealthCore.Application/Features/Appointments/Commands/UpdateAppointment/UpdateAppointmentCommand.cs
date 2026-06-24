using HealthCore.Application.Features.Appointments.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Commands.UpdateAppointment;
public record UpdateAppointmentCommand(Guid Id, UpdateAppointmentDto Dto) : IRequest<AppointmentDto>;