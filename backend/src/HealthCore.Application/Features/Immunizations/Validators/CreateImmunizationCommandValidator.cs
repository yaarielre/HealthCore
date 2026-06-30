using FluentValidation;
using HealthCore.Application.Features.Immunizations.Commands.Create;

namespace HealthCore.Application.Features.Immunizations.Validators;

public class CreateImmunizationCommandValidator : AbstractValidator<CreateImmunizationCommand>
{
    public CreateImmunizationCommandValidator()
    {
        RuleFor(x => x.Dto.PatientId)
            .NotEmpty().WithMessage("El paciente es requerido.");

        RuleFor(x => x.Dto.VaccineName)
            .NotEmpty().WithMessage("El nombre de la vacuna es requerido.")
            .MaximumLength(200).WithMessage("El nombre de la vacuna no puede exceder 200 caracteres.");

        RuleFor(x => x.Dto.DoseNumber)
            .GreaterThan(0).WithMessage("El número de dosis debe ser mayor a 0.");

        RuleFor(x => x.Dto.ApplicationDate)
            .NotEmpty().WithMessage("La fecha de aplicación es requerida.");

        RuleFor(x => x.Dto.BatchNumber)
            .MaximumLength(100).WithMessage("El número de lote no puede exceder 100 caracteres.");

        RuleFor(x => x.Dto.AdministeredBy)
            .MaximumLength(200).WithMessage("El administrador no puede exceder 200 caracteres.");

        RuleFor(x => x.Dto.Notes)
            .MaximumLength(2000).WithMessage("Las notas no pueden exceder 2000 caracteres.");
    }
}
