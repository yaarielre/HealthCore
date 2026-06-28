using FluentValidation.TestHelper;
using HealthCore.Application.Features.MedicalConsultations.Commands.CreateMedicalConsultation;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Application.Features.MedicalConsultations.Validators;

namespace HealthCore.UnitTests.Application.Features.MedicalConsultations.Validators;

public class CreateMedicalConsultationCommandValidatorTests
{
    private readonly CreateMedicalConsultationCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenPatientIdIsEmpty()
    {
        var command = new CreateMedicalConsultationCommand(
            new CreateMedicalConsultationDto(Guid.Empty, Guid.NewGuid(), null, "Reason", null, null, null, null, null, null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.PatientId);
    }

    [Fact]
    public void ShouldHaveError_WhenDoctorIdIsEmpty()
    {
        var command = new CreateMedicalConsultationCommand(
            new CreateMedicalConsultationDto(Guid.NewGuid(), Guid.Empty, null, "Reason", null, null, null, null, null, null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.DoctorId);
    }

    [Fact]
    public void ShouldHaveError_WhenReasonForVisitIsEmpty()
    {
        var command = new CreateMedicalConsultationCommand(
            new CreateMedicalConsultationDto(Guid.NewGuid(), Guid.NewGuid(), null, "", null, null, null, null, null, null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.ReasonForVisit);
    }

    [Fact]
    public void ShouldHaveError_WhenReasonForVisitExceedsMaxLength()
    {
        var command = new CreateMedicalConsultationCommand(
            new CreateMedicalConsultationDto(Guid.NewGuid(), Guid.NewGuid(), null, new string('A', 501), null, null, null, null, null, null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.ReasonForVisit);
    }

    [Fact]
    public void ShouldNotHaveError_WhenValidRequest()
    {
        var command = new CreateMedicalConsultationCommand(
            new CreateMedicalConsultationDto(Guid.NewGuid(), Guid.NewGuid(), null, "Dolor de cabeza", null, null, null, null, null, null));
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ShouldHaveError_WhenTemperatureOutOfRange()
    {
        var command = new CreateMedicalConsultationCommand(
            new CreateMedicalConsultationDto(Guid.NewGuid(), Guid.NewGuid(), null, "Reason", null, null, null, null, null,
                new CreateVitalSignDto(null, 50m, null, null, null, null)));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.VitalSigns!.Temperature);
    }

    [Fact]
    public void ShouldNotHaveError_WhenVitalSignsAreValid()
    {
        var command = new CreateMedicalConsultationCommand(
            new CreateMedicalConsultationDto(Guid.NewGuid(), Guid.NewGuid(), null, "Reason", null, null, null, null, null,
                new CreateVitalSignDto("120/80", 37m, 75m, 170m, 80, 98)));
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
