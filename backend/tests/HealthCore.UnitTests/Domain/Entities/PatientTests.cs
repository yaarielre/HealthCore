using FluentAssertions;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Enums;

namespace HealthCore.UnitTests.Domain.Entities;

public class PatientTests
{
    [Fact]
    public void ShouldSetProperties()
    {
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            FirstName = "Juan",
            LastName = "Perez",
            IdNumber = "00123456789",
            BirthDate = new DateTime(1990, 5, 15),
            Phone = "8095551234",
            Address = "Calle Principal 123",
            Email = "juan@example.com",
            Gender = GenderType.Male,
            BloodType = BloodType.OPositive,
            Allergies = "Penicilina",
            EmergencyContactName = "Maria Perez",
            EmergencyContactPhone = "8095555678"
        };

        patient.FirstName.Should().Be("Juan");
        patient.LastName.Should().Be("Perez");
        patient.Email.Should().Be("juan@example.com");
        patient.Gender.Should().Be(GenderType.Male);
        patient.BloodType.Should().Be(BloodType.OPositive);
        patient.Allergies.Should().Be("Penicilina");
    }

    [Fact]
    public void IsActive_ShouldDefaultToTrue()
    {
        var patient = new Patient();
        patient.IsActive.Should().BeTrue();
    }

    [Fact]
    public void IsDeleted_ShouldDefaultToFalse()
    {
        var patient = new Patient();
        patient.IsDeleted.Should().BeFalse();
    }

    [Fact]
    public void ShouldInitializeCollections()
    {
        var patient = new Patient();

        patient.Appointments.Should().NotBeNull();
        patient.Appointments.Should().BeEmpty();
        patient.MedicalConsultations.Should().NotBeNull();
        patient.MedicalConsultations.Should().BeEmpty();
        patient.Prescriptions.Should().NotBeNull();
        patient.Prescriptions.Should().BeEmpty();
    }
}
