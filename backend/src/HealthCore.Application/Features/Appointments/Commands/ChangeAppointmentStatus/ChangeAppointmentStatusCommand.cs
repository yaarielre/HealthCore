using MediatR;
using HealthCore.Domain.Enums;

namespace HealthCore.Application.Features.Appointments.Commands.ChangeAppointmentStatus;

public record ChangeAppointmentStatusCommand(Guid Id, AppointmentStatus NewStatus) : IRequest<bool>;