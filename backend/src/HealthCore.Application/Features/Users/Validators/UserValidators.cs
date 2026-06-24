using FluentValidation;
using HealthCore.Application.Features.Users.Commands.CreateUser;

namespace HealthCore.Application.Features.Users.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Dto.FirstName)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres.");

        RuleFor(x => x.Dto.LastName)
            .NotEmpty().WithMessage("El apellido es requerido.")
            .MaximumLength(100);

        RuleFor(x => x.Dto.IdNumber)
            .NotEmpty().WithMessage("La cédula es requerida.")
            .MaximumLength(20);

        RuleFor(x => x.Dto.Email)
            .NotEmpty().WithMessage("El correo es requerido.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.")
            .MaximumLength(100);

        RuleFor(x => x.Dto.Phone)
            .MaximumLength(20);

        RuleFor(x => x.Dto.Role)
            .IsInEnum().WithMessage("El rol no es válido.");

        RuleFor(x => x.Dto.Password)
            .NotEmpty().WithMessage("La contraseña es requerida.")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
            .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una mayúscula.")
            .Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una minúscula.")
            .Matches(@"\d").WithMessage("La contraseña debe contener al menos un número.");
    }
}