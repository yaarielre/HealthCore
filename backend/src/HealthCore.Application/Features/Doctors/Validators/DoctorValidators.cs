using FluentValidation;
using HealthCore.Application.Features.Doctors.Commands.CreateDoctor;
using HealthCore.Application.Features.Doctors.Commands.UpdateDoctor;

namespace HealthCore.Application.Features.Doctors.Validators;

public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es requerido.")
            .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres.");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty().WithMessage("El número de exequátur es requerido.")
            .MaximumLength(50).WithMessage("El exequátur no puede exceder 50 caracteres.");

        RuleFor(x => x.SpecialtyId)
            .NotEmpty().WithMessage("La especialidad es requerida.");
    }
}

public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id es requerido.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es requerido.")
            .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres.");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty().WithMessage("El número de exequátur es requerido.")
            .MaximumLength(50).WithMessage("El exequátur no puede exceder 50 caracteres.");

        RuleFor(x => x.SpecialtyId)
            .NotEmpty().WithMessage("La especialidad es requerida.");
    }
}