using FluentValidation;
using HealthCore.Application.Features.Users.DTOs;

namespace HealthCore.Application.Features.Users.Validators;
public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(X => X.FirstName)
            .NotEmpty().WithMessage("El Nombre es requerido.")
            .MaximumLength(50).WithMessage("El Nombre no puede tener más de 50 caracteres.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es requerido.")
            .MaximumLength(100);

        RuleFor(x => x.IdNumber)
            .NotEmpty().WithMessage("La cédula es requerida.")
            .MaximumLength(20);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es requerido.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.")
            .MaximumLength(100);

        RuleFor(x => x.Phone)
            .MaximumLength(20);

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("El rol no es válido.");
    }

}   