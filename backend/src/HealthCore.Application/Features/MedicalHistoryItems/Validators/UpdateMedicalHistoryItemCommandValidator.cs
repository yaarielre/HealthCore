using FluentValidation;
using HealthCore.Application.Features.MedicalHistoryItems.Commands.Update;

namespace HealthCore.Application.Features.MedicalHistoryItems.Validators;

public class UpdateMedicalHistoryItemCommandValidator : AbstractValidator<UpdateMedicalHistoryItemCommand>
{
    public UpdateMedicalHistoryItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id del item de historial es requerido.");

        RuleFor(x => x.Dto.Category)
            .IsInEnum().WithMessage("La categoría no es válida.");

        RuleFor(x => x.Dto.Description)
            .NotEmpty().WithMessage("La descripción es requerida.")
            .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres.");

        RuleFor(x => x.Dto.Details)
            .MaximumLength(2000).WithMessage("Los detalles no pueden exceder 2000 caracteres.");

        RuleFor(x => x.Dto.Severity)
            .InclusiveBetween(1, 10).When(x => x.Dto.Severity.HasValue)
            .WithMessage("La severidad debe estar entre 1 y 10.");
    }
}
