using FluentValidation;
using HealthCore.Application.Features.MedicalConsultations.Commands.UpdateMedicalConsultation;

namespace HealthCore.Application.Features.MedicalConsultations.Validators;

public class UpdateMedicalConsultationCommandValidator : AbstractValidator<UpdateMedicalConsultationCommand>
{
    public UpdateMedicalConsultationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id de la consulta es requerido.");

        RuleFor(x => x.Dto.DoctorId)
            .NotEmpty().WithMessage("El médico es requerido.");

        RuleFor(x => x.Dto.ReasonForVisit)
            .NotEmpty().WithMessage("El motivo de consulta es requerido.")
            .MaximumLength(500).WithMessage("El motivo no puede exceder 500 caracteres.");

        RuleFor(x => x.Dto.Symptoms)
            .MaximumLength(2000);

        RuleFor(x => x.Dto.Diagnosis)
            .MaximumLength(2000);

        RuleFor(x => x.Dto.Treatment)
            .MaximumLength(2000);

        RuleFor(x => x.Dto.Observations)
            .MaximumLength(2000);

        RuleFor(x => x.Dto.InternalNotes)
            .MaximumLength(2000);

        When(x => x.Dto.VitalSigns is not null, () =>
        {
            RuleFor(x => x.Dto.VitalSigns!.BloodPressure)
                .MaximumLength(20);

            RuleFor(x => x.Dto.VitalSigns!.Temperature)
                .InclusiveBetween(30, 45).When(x => x.Dto.VitalSigns!.Temperature.HasValue)
                .WithMessage("La temperatura debe estar entre 30 y 45 °C.");

            RuleFor(x => x.Dto.VitalSigns!.Weight)
                .InclusiveBetween(1, 500).When(x => x.Dto.VitalSigns!.Weight.HasValue)
                .WithMessage("El peso debe estar entre 1 y 500 kg.");

            RuleFor(x => x.Dto.VitalSigns!.Height)
                .InclusiveBetween(20, 250).When(x => x.Dto.VitalSigns!.Height.HasValue)
                .WithMessage("La estatura debe estar entre 20 y 250 cm.");

            RuleFor(x => x.Dto.VitalSigns!.HeartRate)
                .InclusiveBetween(20, 300).When(x => x.Dto.VitalSigns!.HeartRate.HasValue)
                .WithMessage("La frecuencia cardíaca debe estar entre 20 y 300 lpm.");

            RuleFor(x => x.Dto.VitalSigns!.OxygenSaturation)
                .InclusiveBetween(50, 100).When(x => x.Dto.VitalSigns!.OxygenSaturation.HasValue)
                .WithMessage("La saturación de oxígeno debe estar entre 50 y 100%.");
        });
    }
}
