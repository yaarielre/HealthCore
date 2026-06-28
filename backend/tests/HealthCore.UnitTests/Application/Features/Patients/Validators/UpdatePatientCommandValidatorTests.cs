using FluentValidation.TestHelper;
using HealthCore.Application.Features.Patients.Commands.UpdatePatient;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Application.Features.Patients.Validators;

namespace HealthCore.UnitTests.Application.Features.Patients.Validators;

public class UpdatePatientCommandValidatorTests
{
    private readonly UpdatePatientCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenFirstNameIsEmpty()
    {
        var command = new UpdatePatientCommand(Guid.NewGuid(),
            new UpdatePatientDto { FirstName = "", LastName = "Perez", Phone = "8095551234", Address = "Calle" });
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.FirstName);
    }

    [Fact]
    public void ShouldNotHaveError_WhenValidRequest()
    {
        var command = new UpdatePatientCommand(Guid.NewGuid(),
            new UpdatePatientDto
            {
                FirstName = "Juan",
                LastName = "Perez",
                Phone = "8095551234",
                Address = "Calle Principal"
            });
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
