using FluentValidation;
using HealthCore.Application.Features.Prescriptions.Commands.UpdatePrescription;

namespace HealthCore.Application.Features.Prescriptions.Validators;

public class UpdatePrescriptionCommandValidator : AbstractValidator<UpdatePrescriptionCommand>
{
    public UpdatePrescriptionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id de la receta es requerido.");

        RuleFor(x => x.Dto.DoctorId)
            .NotEmpty().WithMessage("El médico es requerido.");

        RuleFor(x => x.Dto.MedicalConsultationId)
            .NotEmpty().WithMessage("La consulta es requerida.");

        RuleFor(x => x.Dto.Medication)
            .NotEmpty().WithMessage("El medicamento es requerido.")
            .MaximumLength(300).WithMessage("El medicamento no puede exceder 300 caracteres.");

        RuleFor(x => x.Dto.Dosage)
            .NotEmpty().WithMessage("La dosis es requerida.")
            .MaximumLength(200).WithMessage("La dosis no puede exceder 200 caracteres.");

        RuleFor(x => x.Dto.Frequency)
            .NotEmpty().WithMessage("La frecuencia es requerida.")
            .MaximumLength(200).WithMessage("La frecuencia no puede exceder 200 caracteres.");

        RuleFor(x => x.Dto.Duration)
            .NotEmpty().WithMessage("La duración es requerida.")
            .MaximumLength(200).WithMessage("La duración no puede exceder 200 caracteres.");

        RuleFor(x => x.Dto.Instructions)
            .MaximumLength(1000).WithMessage("Las instrucciones no pueden exceder 1000 caracteres.");
    }
}
