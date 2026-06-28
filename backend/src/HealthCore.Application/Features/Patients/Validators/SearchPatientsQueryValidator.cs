using FluentValidation;
using HealthCore.Application.Features.Patients.Queries.SearchPatients;

namespace HealthCore.Application.Features.Patients.Validators;

public class SearchPatientsQueryValidator : AbstractValidator<SearchPatientsQuery>
{
    public SearchPatientsQueryValidator()
    {
        RuleFor(x => x.Term)
            .NotEmpty().WithMessage("El término de búsqueda es requerido")
            .MinimumLength(2).WithMessage("El término de búsqueda debe tener al menos 2 caracteres");
    }
}
