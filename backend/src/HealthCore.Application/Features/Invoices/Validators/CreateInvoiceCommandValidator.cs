using FluentValidation;
using HealthCore.Application.Features.Invoices.Commands.Create;

namespace HealthCore.Application.Features.Invoices.Validators;

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("El paciente es requerido.");

        RuleFor(x => x.IssuedById)
            .NotEmpty().WithMessage("El usuario emisor es requerido.");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.MinValue).WithMessage("La fecha de vencimiento es requerida.");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("El monto total debe ser mayor a cero.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("La factura debe tener al menos un item.");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.Description)
                .NotEmpty().WithMessage("La descripción del item es requerida.")
                .MaximumLength(200).WithMessage("La descripción no puede exceder 200 caracteres.");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");

            item.RuleFor(i => i.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("El precio unitario no puede ser negativo.");
        });
    }
}
