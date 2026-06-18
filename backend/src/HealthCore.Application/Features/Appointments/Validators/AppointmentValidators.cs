using FluentValidation;
using HealthCore.Application.Features.Appointments.DTOs;

namespace HealthCore.Application.Features.Appointment.Validators
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("El paciente es requerido.");

            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("El médico es requerido.");

            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("La fecha es requerida.")
                .GreaterThan(DateTime.UtcNow).WithMessage("La fecha debe ser futura.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("El motivo de consulta es requerido.")
                .MaximumLength(500);

            RuleFor(x => x.Notes)
                .MaximumLength(1000);
        }
    }
}
