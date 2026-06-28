using MediatR;

namespace HealthCore.Application.Features.Patients.Commands.RestorePatient;

public record RestorePatientCommand(Guid Id) : IRequest<bool>;
