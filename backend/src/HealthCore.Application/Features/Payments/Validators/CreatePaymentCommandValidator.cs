using FluentValidation;
using HealthCore.Application.Features.Payments.Commands.Create;

namespace HealthCore.Application.Features.Payments.Validators;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.InvoiceId)
            .NotEmpty().WithMessage("La factura es requerida.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a cero.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("El método de pago no es válido.");

        RuleFor(x => x.ReceivedById)
            .NotEmpty().WithMessage("El usuario que recibe es requerido.");

        RuleFor(x => x.ReferenceNumber)
            .MaximumLength(100).WithMessage("El número de referencia no puede exceder 100 caracteres.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Las notas no pueden exceder 500 caracteres.");
    }
}
