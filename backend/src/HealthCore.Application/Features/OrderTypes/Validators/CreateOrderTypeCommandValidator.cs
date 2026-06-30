using FluentValidation;
using HealthCore.Application.Features.OrderTypes.Commands.Create;

namespace HealthCore.Application.Features.OrderTypes.Validators;

public class CreateOrderTypeCommandValidator : AbstractValidator<CreateOrderTypeCommand>
{
    public CreateOrderTypeCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("El nombre del tipo de orden es requerido.")
            .MaximumLength(200).WithMessage("El nombre no puede exceder 200 caracteres.");

        RuleFor(x => x.Dto.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres.");
    }
}
