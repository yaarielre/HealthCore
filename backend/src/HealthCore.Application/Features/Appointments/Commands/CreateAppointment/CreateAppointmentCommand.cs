using MediatR;
using HealthCore.Application.Features.Appointments.DTOs;

namespace HealthCore.Application.Features.Appointment.Commands.CreateAppointment;
    public record CreateAppointmentCommand(CreateAppointmentDto Dto) : IRequest<AppointmentDto>;

