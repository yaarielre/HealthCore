using FluentValidation;
using HealthCore.Application.Features.InsuranceClaims.Commands.Create;

namespace HealthCore.Application.Features.InsuranceClaims.Validators;

public class CreateInsuranceClaimCommandValidator : AbstractValidator<CreateInsuranceClaimCommand>
{
    public CreateInsuranceClaimCommandValidator()
    {
        RuleFor(x => x.InvoiceId)
            .NotEmpty().WithMessage("La factura es requerida.");

        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("El paciente es requerido.");

        RuleFor(x => x.InsuranceCompany)
            .NotEmpty().WithMessage("La compañía de seguros es requerida.")
            .MaximumLength(100).WithMessage("La compañía no puede exceder 100 caracteres.");

        RuleFor(x => x.PolicyNumber)
            .NotEmpty().WithMessage("El número de póliza es requerido.")
            .MaximumLength(50).WithMessage("El número de póliza no puede exceder 50 caracteres.");

        RuleFor(x => x.ClaimAmount)
            .GreaterThan(0).WithMessage("El monto del reclamo debe ser mayor a cero.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Las notas no pueden exceder 500 caracteres.");
    }
}
