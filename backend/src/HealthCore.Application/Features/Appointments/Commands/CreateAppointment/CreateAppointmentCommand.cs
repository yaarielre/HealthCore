using MediatR;
using HealthCore.Application.Features.Appointments.DTOs;

namespace HealthCore.Application.Features.Appointments.Commands.CreateAppointment;
    public record CreateAppointmentCommand(CreateAppointmentDto Dto) : IRequest<AppointmentDto>;

