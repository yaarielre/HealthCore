using FluentAssertions;
using HealthCore.Domain.Entities;

namespace HealthCore.UnitTests.Domain.Entities;

public class PrescriptionTests
{
    [Fact]
    public void ShouldSetProperties()
    {
        var prescription = new Prescription
        {
            Id = Guid.NewGuid(),
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            MedicalConsultationId = Guid.NewGuid(),
            Medication = "Amoxicilina",
            Dosage = "500mg",
            Frequency = "Cada 8 horas",
            Duration = "7 días",
            Instructions = "Tomar con alimentos"
        };

        prescription.Medication.Should().Be("Amoxicilina");
        prescription.Dosage.Should().Be("500mg");
        prescription.Frequency.Should().Be("Cada 8 horas");
        prescription.Duration.Should().Be("7 días");
        prescription.Instructions.Should().Be("Tomar con alimentos");
    }

    [Fact]
    public void DefaultValues_ShouldBeEmptyString_WhenNotSet()
    {
        var prescription = new Prescription();

        prescription.Medication.Should().BeEmpty();
        prescription.Dosage.Should().BeEmpty();
        prescription.Frequency.Should().BeEmpty();
        prescription.Duration.Should().BeEmpty();
    }
}
