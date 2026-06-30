using FluentValidation;
using HealthCore.Application.Features.Orders.Commands.Update;

namespace HealthCore.Application.Features.Orders.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id de la orden es requerido.");

        RuleFor(x => x.Dto.OrderTypeId)
            .NotEmpty().WithMessage("El tipo de orden es requerido.");

        RuleFor(x => x.Dto.Priority)
            .IsInEnum().WithMessage("La prioridad no es válida.");

        RuleFor(x => x.Dto.Status)
            .IsInEnum().WithMessage("El estado no es válido.");

        RuleFor(x => x.Dto.Notes)
            .MaximumLength(1000).WithMessage("Las notas no pueden exceder 1000 caracteres.");
    }
}
