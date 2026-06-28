using FluentValidation.TestHelper;
using HealthCore.Application.Features.Prescriptions.Commands.CreatePrescription;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Application.Features.Prescriptions.Validators;

namespace HealthCore.UnitTests.Application.Features.Prescriptions.Validators;

public class CreatePrescriptionCommandValidatorTests
{
    private readonly CreatePrescriptionCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenPatientIdIsEmpty()
    {
        var command = new CreatePrescriptionCommand(
            new CreatePrescriptionDto(Guid.Empty, Guid.NewGuid(), Guid.NewGuid(), "Med", "Dose", "Freq", "Dur", null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.PatientId);
    }

    [Fact]
    public void ShouldHaveError_WhenDoctorIdIsEmpty()
    {
        var command = new CreatePrescriptionCommand(
            new CreatePrescriptionDto(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), "Med", "Dose", "Freq", "Dur", null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.DoctorId);
    }

    [Fact]
    public void ShouldHaveError_WhenMedicationIsEmpty()
    {
        var command = new CreatePrescriptionCommand(
            new CreatePrescriptionDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "", "Dose", "Freq", "Dur", null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.Medication);
    }

    [Fact]
    public void ShouldHaveError_WhenDosageIsEmpty()
    {
        var command = new CreatePrescriptionCommand(
            new CreatePrescriptionDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Med", "", "Freq", "Dur", null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.Dosage);
    }

    [Fact]
    public void ShouldHaveError_WhenFrequencyIsEmpty()
    {
        var command = new CreatePrescriptionCommand(
            new CreatePrescriptionDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Med", "Dose", "", "Dur", null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.Frequency);
    }

    [Fact]
    public void ShouldHaveError_WhenDurationIsEmpty()
    {
        var command = new CreatePrescriptionCommand(
            new CreatePrescriptionDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Med", "Dose", "Freq", "", null));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.Duration);
    }

    [Fact]
    public void ShouldNotHaveError_WhenValidRequest()
    {
        var command = new CreatePrescriptionCommand(
            new CreatePrescriptionDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Amoxicilina", "500mg", "Cada 8h", "7 días", "Tomar con alimentos"));
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ShouldHaveError_WhenInstructionsExceedsMaxLength()
    {
        var command = new CreatePrescriptionCommand(
            new CreatePrescriptionDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Med", "Dose", "Freq", "Dur", new string('A', 501)));
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.Instructions);
    }
}
