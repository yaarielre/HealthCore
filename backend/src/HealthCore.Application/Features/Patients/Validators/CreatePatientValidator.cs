using FluentValidation;
using HealthCore.Application.Features.Patients.Commands.CreatePatient;

namespace HealthCore.Application.Features.Patients.Validators;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.Dto.FirstName)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.Dto.LastName)
            .NotEmpty().WithMessage("El apellido es requerido")
            .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres");

        RuleFor(x => x.Dto.Cedula)
            .NotEmpty().WithMessage("La cédula es requerida")
            .Length(11).WithMessage("La cédula debe tener 11 dígitos")
            .Matches(@"^\d+$").WithMessage("La cédula solo puede contener números");

        RuleFor(x => x.Dto.BirthDate)
            .NotEmpty().WithMessage("La fecha de nacimiento es requerida")
            .LessThan(DateTime.UtcNow).WithMessage("La fecha de nacimiento no puede ser en el futuro");

        RuleFor(x => x.Dto.Phone)
            .NotEmpty().WithMessage("El teléfono es requerido")
            .Matches(@"^\d{10}$").WithMessage("El teléfono debe tener 10 dígitos");

        RuleFor(x => x.Dto.Address)
            .NotEmpty().WithMessage("La dirección es requerida")
            .MaximumLength(250).WithMessage("La dirección no puede exceder 250 caracteres");
    }
}