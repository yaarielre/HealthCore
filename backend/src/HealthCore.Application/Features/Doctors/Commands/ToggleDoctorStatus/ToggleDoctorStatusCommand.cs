using MediatR;

namespace HealthCore.Application.Features.Doctors.Commands.ToggleDoctorStatus;

public record ToggleDoctorStatusCommand(Guid Id) : IRequest<bool>;