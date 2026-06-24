using FluentValidation;
using HealthCore.Application.Features.Specialties.Commands.CreateSpecialty;
using HealthCore.Application.Features.Specialties.Commands.UpdateSpecialty;

namespace HealthCore.Application.Features.Specialties.Validators;

public class CreateSpecialtyCommandValidator : AbstractValidator<CreateSpecialtyCommand>
{
    public CreateSpecialtyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la especialidad es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(300).WithMessage("La descripción no puede exceder 300 caracteres.");
    }
}

public class UpdateSpecialtyCommandValidator : AbstractValidator<UpdateSpecialtyCommand>
{
    public UpdateSpecialtyCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id es requerido.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la especialidad es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(300).WithMessage("La descripción no puede exceder 300 caracteres.");
    }
}