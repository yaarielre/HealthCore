using FluentValidation;
using HealthCore.Application.Features.MedicalRecords.Commands.CreateMedicalRecord;

namespace HealthCore.Application.Features.MedicalRecords.Validators;

public class CreateMedicalRecordCommandValidator : AbstractValidator<CreateMedicalRecordCommand>
{
    public CreateMedicalRecordCommandValidator()
    {
        RuleFor(x => x.Dto.PatientId)
            .NotEmpty().WithMessage("El paciente es requerido.");

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
