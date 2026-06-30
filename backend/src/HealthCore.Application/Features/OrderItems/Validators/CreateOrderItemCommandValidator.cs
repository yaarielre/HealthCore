using FluentValidation;
using HealthCore.Application.Features.OrderItems.Commands.Create;

namespace HealthCore.Application.Features.OrderItems.Validators;

public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("La orden es requerida.");

        RuleFor(x => x.Dto.ItemName)
            .NotEmpty().WithMessage("El nombre del item es requerido.")
            .MaximumLength(300).WithMessage("El nombre del item no puede exceder 300 caracteres.");

        RuleFor(x => x.Dto.Description)
            .MaximumLength(1000).WithMessage("La descripción no puede exceder 1000 caracteres.");

        RuleFor(x => x.Dto.Quantity)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

        RuleFor(x => x.Dto.UnitPrice)
            .GreaterThanOrEqualTo(0).When(x => x.Dto.UnitPrice.HasValue)
            .WithMessage("El precio unitario no puede ser negativo.");
    }
}
