using FluentValidation;
using HealthCore.Application.Features.OrderItems.Commands.Update;

namespace HealthCore.Application.Features.OrderItems.Validators;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id del item es requerido.");

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

        RuleFor(x => x.Dto.Results)
            .MaximumLength(5000).WithMessage("Los resultados no pueden exceder 5000 caracteres.");

        RuleFor(x => x.Dto.ResultUrl)
            .MaximumLength(1000).WithMessage("La URL del resultado no puede exceder 1000 caracteres.");
    }
}
