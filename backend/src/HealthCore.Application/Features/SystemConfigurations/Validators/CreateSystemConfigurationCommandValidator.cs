using FluentValidation;
using HealthCore.Application.Features.SystemConfigurations.Commands.CreateSystemConfiguration;

namespace HealthCore.Application.Features.SystemConfigurations.Validators;

public class CreateSystemConfigurationCommandValidator : AbstractValidator<CreateSystemConfigurationCommand>
{
    public CreateSystemConfigurationCommandValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("La categoría es requerida.")
            .MaximumLength(100).WithMessage("La categoría no puede exceder los 100 caracteres.");

        RuleFor(x => x.ConfigKey)
            .NotEmpty().WithMessage("La clave es requerida.")
            .MaximumLength(200).WithMessage("La clave no puede exceder los 200 caracteres.");

        RuleFor(x => x.ConfigValue)
            .NotEmpty().WithMessage("El valor es requerido.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");
    }
}
