using FluentValidation;
using HealthCore.Application.Features.Orders.Commands.Create;

namespace HealthCore.Application.Features.Orders.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Dto.PatientId)
            .NotEmpty().WithMessage("El paciente es requerido.");

        RuleFor(x => x.Dto.DoctorId)
            .NotEmpty().WithMessage("El médico es requerido.");

        RuleFor(x => x.Dto.OrderTypeId)
            .NotEmpty().WithMessage("El tipo de orden es requerido.");

        RuleFor(x => x.Dto.Priority)
            .IsInEnum().WithMessage("La prioridad no es válida.");

        RuleFor(x => x.Dto.Notes)
            .MaximumLength(1000).WithMessage("Las notas no pueden exceder 1000 caracteres.");

        RuleFor(x => x.Dto.Items)
            .NotEmpty().WithMessage("Debe incluir al menos un item en la orden.");

        RuleForEach(x => x.Dto.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ItemName)
                .NotEmpty().WithMessage("El nombre del item es requerido.")
                .MaximumLength(300).WithMessage("El nombre del item no puede exceder 300 caracteres.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");

            item.RuleFor(i => i.UnitPrice)
                .GreaterThanOrEqualTo(0).When(i => i.UnitPrice.HasValue)
                .WithMessage("El precio unitario no puede ser negativo.");
        });
    }
}
