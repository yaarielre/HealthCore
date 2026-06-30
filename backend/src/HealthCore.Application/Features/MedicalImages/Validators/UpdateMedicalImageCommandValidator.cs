using FluentValidation;
using HealthCore.Application.Features.MedicalImages.Commands.Update;

namespace HealthCore.Application.Features.MedicalImages.Validators;

public class UpdateMedicalImageCommandValidator : AbstractValidator<UpdateMedicalImageCommand>
{
    public UpdateMedicalImageCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id de la imagen médica es requerido.");

        RuleFor(x => x.Dto.ImageType)
            .NotEmpty().WithMessage("El tipo de imagen es requerido.")
            .MaximumLength(100).WithMessage("El tipo de imagen no puede exceder 100 caracteres.");

        RuleFor(x => x.Dto.BodyPart)
            .MaximumLength(200).WithMessage("La parte del cuerpo no puede exceder 200 caracteres.");

        RuleFor(x => x.Dto.FileName)
            .NotEmpty().WithMessage("El nombre del archivo es requerido.")
            .MaximumLength(500).WithMessage("El nombre del archivo no puede exceder 500 caracteres.");

        RuleFor(x => x.Dto.FilePath)
            .NotEmpty().WithMessage("La ruta del archivo es requerida.")
            .MaximumLength(1000).WithMessage("La ruta del archivo no puede exceder 1000 caracteres.");

        RuleFor(x => x.Dto.ContentType)
            .NotEmpty().WithMessage("El tipo de contenido es requerido.")
            .MaximumLength(100).WithMessage("El tipo de contenido no puede exceder 100 caracteres.");

        RuleFor(x => x.Dto.FileSizeBytes)
            .GreaterThan(0).WithMessage("El tamaño del archivo debe ser mayor a 0.");

        RuleFor(x => x.Dto.Description)
            .MaximumLength(2000).WithMessage("La descripción no puede exceder 2000 caracteres.");

        RuleFor(x => x.Dto.Interpretation)
            .MaximumLength(5000).WithMessage("La interpretación no puede exceder 5000 caracteres.");
    }
}
