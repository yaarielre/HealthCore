using FluentAssertions;
using HealthCore.Domain.Entities;

namespace HealthCore.UnitTests.Domain.Entities;

public class VitalSignTests
{
    [Fact]
    public void ShouldSetProperties()
    {
        var vitalSign = new VitalSign
        {
            Id = Guid.NewGuid(),
            MedicalConsultationId = Guid.NewGuid(),
            BloodPressure = "130/85",
            Temperature = 36.6m,
            Weight = 80m,
            Height = 175m,
            HeartRate = 72,
            OxygenSaturation = 97
        };

        vitalSign.BloodPressure.Should().Be("130/85");
        vitalSign.Temperature.Should().Be(36.6m);
        vitalSign.Weight.Should().Be(80m);
        vitalSign.Height.Should().Be(175m);
        vitalSign.HeartRate.Should().Be(72);
        vitalSign.OxygenSaturation.Should().Be(97);
    }

    [Fact]
    public void NullableProperties_ShouldBeNullByDefault()
    {
        var vitalSign = new VitalSign();

        vitalSign.BloodPressure.Should().BeNull();
        vitalSign.Temperature.Should().BeNull();
        vitalSign.Weight.Should().BeNull();
        vitalSign.Height.Should().BeNull();
        vitalSign.HeartRate.Should().BeNull();
        vitalSign.OxygenSaturation.Should().BeNull();
    }
}
