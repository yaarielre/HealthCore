using FluentAssertions;
using HealthCore.Domain.Entities;

namespace HealthCore.UnitTests.Domain.Entities;

public class MedicalConsultationTests
{
    [Fact]
    public void Constructor_ShouldInitializeCollections()
    {
        var consultation = new MedicalConsultation();

        consultation.VitalSigns.Should().NotBeNull();
        consultation.VitalSigns.Should().BeEmpty();
        consultation.Prescriptions.Should().NotBeNull();
        consultation.Prescriptions.Should().BeEmpty();
    }

    [Fact]
    public void ShouldSetProperties()
    {
        var patientId = Guid.NewGuid();
        var doctorId = Guid.NewGuid();
        var consultation = new MedicalConsultation
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            DoctorId = doctorId,
            ReasonForVisit = "Dolor de cabeza",
            Symptoms = "Dolor punzante",
            Diagnosis = "Migraña",
            Treatment = "Reposo",
            Observations = "Ninguna",
            InternalNotes = "Nota interna"
        };

        consultation.PatientId.Should().Be(patientId);
        consultation.DoctorId.Should().Be(doctorId);
        consultation.ReasonForVisit.Should().Be("Dolor de cabeza");
        consultation.Symptoms.Should().Be("Dolor punzante");
        consultation.Diagnosis.Should().Be("Migraña");
        consultation.Treatment.Should().Be("Reposo");
    }

    [Fact]
    public void ShouldAddVitalSign()
    {
        var consultation = new MedicalConsultation();
        var vitalSign = new VitalSign
        {
            Id = Guid.NewGuid(),
            MedicalConsultationId = consultation.Id,
            BloodPressure = "120/80",
            Temperature = 37.5m,
            Weight = 75m,
            Height = 170m,
            HeartRate = 80,
            OxygenSaturation = 98
        };

        consultation.VitalSigns.Add(vitalSign);

        consultation.VitalSigns.Should().ContainSingle();
        consultation.VitalSigns.First().BloodPressure.Should().Be("120/80");
    }
}
