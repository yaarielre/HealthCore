using FluentValidation.TestHelper;
using HealthCore.Application.Features.Patients.Queries.SearchPatients;
using HealthCore.Application.Features.Patients.Validators;

namespace HealthCore.UnitTests.Application.Features.Patients.Validators;

public class SearchPatientsQueryValidatorTests
{
    private readonly SearchPatientsQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenTermIsEmpty()
    {
        var query = new SearchPatientsQuery("");
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Term);
    }

    [Fact]
    public void ShouldHaveError_WhenTermIsLessThan2Chars()
    {
        var query = new SearchPatientsQuery("a");
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Term);
    }

    [Fact]
    public void ShouldNotHaveError_WhenTermIsValid()
    {
        var query = new SearchPatientsQuery("Ju");
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
