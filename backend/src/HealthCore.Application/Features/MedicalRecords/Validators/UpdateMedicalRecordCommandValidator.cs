using FluentValidation;
using HealthCore.Application.Features.MedicalRecords.Commands.UpdateMedicalRecord;

namespace HealthCore.Application.Features.MedicalRecords.Validators;

public class UpdateMedicalRecordCommandValidator : AbstractValidator<UpdateMedicalRecordCommand>
{
    public UpdateMedicalRecordCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id del historial clínico es requerido.");

        RuleFor(x => x.Dto.RecordNumber)
            .NotEmpty().WithMessage("El número de historial es requerido.")
            .MaximumLength(50).WithMessage("El número de historial no puede exceder 50 caracteres.");

        RuleFor(x => x.Dto.Allergies)
            .MaximumLength(500).WithMessage("Las alergias no pueden exceder 500 caracteres.");

        RuleFor(x => x.Dto.EmergencyContactName)
            .MaximumLength(200).WithMessage("El nombre del contacto de emergencia no puede exceder 200 caracteres.");

        RuleFor(x => x.Dto.EmergencyContactPhone)
            .MaximumLength(20).WithMessage("El teléfono de emergencia no puede exceder 20 caracteres.");

        RuleFor(x => x.Dto.Notes)
            .MaximumLength(2000).WithMessage("Las notas no pueden exceder 2000 caracteres.");
    }
}
