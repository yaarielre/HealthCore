using FluentValidation;
using HealthCore.Application.Features.Prescriptions.Commands.CreatePrescription;

namespace HealthCore.Application.Features.Prescriptions.Validators;

public class CreatePrescriptionCommandValidator : AbstractValidator<CreatePrescriptionCommand>
{
    public CreatePrescriptionCommandValidator()
    {
        RuleFor(x => x.Dto.PatientId)
            .NotEmpty().WithMessage("El paciente es requerido.");

        RuleFor(x => x.Dto.DoctorId)
            .NotEmpty().WithMessage("El médico es requerido.");

        RuleFor(x => x.Dto.MedicalConsultationId)
            .NotEmpty().WithMessage("La consulta médica es requerida.");

        RuleFor(x => x.Dto.Medication)
            .NotEmpty().WithMessage("El medicamento es requerido.")
            .MaximumLength(200).WithMessage("El medicamento no puede exceder 200 caracteres.");

        RuleFor(x => x.Dto.Dosage)
            .NotEmpty().WithMessage("La dosis es requerida.")
            .MaximumLength(100).WithMessage("La dosis no puede exceder 100 caracteres.");

        RuleFor(x => x.Dto.Frequency)
            .NotEmpty().WithMessage("La frecuencia es requerida.")
            .MaximumLength(100).WithMessage("La frecuencia no puede exceder 100 caracteres.");

        RuleFor(x => x.Dto.Duration)
            .NotEmpty().WithMessage("La duración es requerida.")
            .MaximumLength(100).WithMessage("La duración no puede exceder 100 caracteres.");

        RuleFor(x => x.Dto.Instructions)
            .MaximumLength(500).WithMessage("Las indicaciones no pueden exceder 500 caracteres.");
    }
}
