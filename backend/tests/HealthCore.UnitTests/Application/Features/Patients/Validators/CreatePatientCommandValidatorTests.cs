using FluentValidation.TestHelper;
using HealthCore.Application.Features.Patients.Commands.CreatePatient;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Application.Features.Patients.Validators;

namespace HealthCore.UnitTests.Application.Features.Patients.Validators;

public class CreatePatientCommandValidatorTests
{
    private readonly CreatePatientCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenFirstNameIsEmpty()
    {
        var command = new CreatePatientCommand(
            new CreatePatientDto { FirstName = "", LastName = "Perez", IdNumber = "00123456789", BirthDate = new DateTime(1990, 1, 1), Phone = "8095551234", Address = "Calle" });
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.FirstName);
    }

    [Fact]
    public void ShouldHaveError_WhenLastNameIsEmpty()
    {
        var command = new CreatePatientCommand(
            new CreatePatientDto { FirstName = "Juan", LastName = "", IdNumber = "00123456789", BirthDate = new DateTime(1990, 1, 1), Phone = "8095551234", Address = "Calle" });
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.LastName);
    }

    [Fact]
    public void ShouldHaveError_WhenIdNumberIsNot11Digits()
    {
        var command = new CreatePatientCommand(
            new CreatePatientDto { FirstName = "Juan", LastName = "Perez", IdNumber = "123", BirthDate = new DateTime(1990, 1, 1), Phone = "8095551234", Address = "Calle" });
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.IdNumber);
    }

    [Fact]
    public void ShouldHaveError_WhenIdNumberHasNonDigits()
    {
        var command = new CreatePatientCommand(
            new CreatePatientDto { FirstName = "Juan", LastName = "Perez", IdNumber = "0012345678A", BirthDate = new DateTime(1990, 1, 1), Phone = "8095551234", Address = "Calle" });
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.IdNumber);
    }

    [Fact]
    public void ShouldHaveError_WhenBirthDateIsInFuture()
    {
        var command = new CreatePatientCommand(
            new CreatePatientDto { FirstName = "Juan", LastName = "Perez", IdNumber = "00123456789", BirthDate = DateTime.UtcNow.AddDays(1), Phone = "8095551234", Address = "Calle" });
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.BirthDate);
    }

    [Fact]
    public void ShouldHaveError_WhenPhoneIsNot10Digits()
    {
        var command = new CreatePatientCommand(
            new CreatePatientDto { FirstName = "Juan", LastName = "Perez", IdNumber = "00123456789", BirthDate = new DateTime(1990, 1, 1), Phone = "123", Address = "Calle" });
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.Phone);
    }

    [Fact]
    public void ShouldNotHaveError_WhenValidRequest()
    {
        var command = new CreatePatientCommand(
            new CreatePatientDto
            {
                FirstName = "Juan",
                LastName = "Perez",
                IdNumber = "00123456789",
                BirthDate = new DateTime(1990, 1, 1),
                Phone = "8095551234",
                Address = "Calle Principal 123"
            });
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
