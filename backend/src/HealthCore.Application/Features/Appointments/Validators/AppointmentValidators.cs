using FluentValidation;
using HealthCore.Application.Features.Appointments.Commands.CreateAppointment;

namespace HealthCore.Application.Features.Appointments.Validators;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.Dto.PatientId)
            .NotEmpty().WithMessage("El paciente es requerido.");

        RuleFor(x => x.Dto.DoctorId)
            .NotEmpty().WithMessage("El médico es requerido.");

        RuleFor(x => x.Dto.AppointmentDate)
            .NotEmpty().WithMessage("La fecha es requerida.")
            .GreaterThan(DateTime.UtcNow).WithMessage("La fecha debe ser futura.");

        RuleFor(x => x.Dto.Reason)
            .NotEmpty().WithMessage("El motivo de consulta es requerido.")
            .MaximumLength(500);

        RuleFor(x => x.Dto.Notes)
            .MaximumLength(1000);
    }
}
