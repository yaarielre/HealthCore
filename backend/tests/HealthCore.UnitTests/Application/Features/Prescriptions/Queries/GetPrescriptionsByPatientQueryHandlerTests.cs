using AutoMapper;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionsByPatient;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.Prescriptions.Queries;

public class GetPrescriptionsByPatientQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPrescriptionsForPatient()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetPrescriptionsByPatientQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
        var patientId = Guid.NewGuid();
        var query = new GetPrescriptionsByPatientQuery(patientId);
        var prescriptions = new List<Prescription> { new() { Id = Guid.NewGuid(), PatientId = patientId } };
        var dtos = new List<PrescriptionDto>
        {
            new(Guid.NewGuid(), patientId, "P", Guid.NewGuid(), "D",
                Guid.NewGuid(), "Med", "Dose", "Freq", "Dur", null, DateTime.UtcNow)
        };

        unitOfWorkMock.Setup(x => x.Prescriptions.GetByPatientIdAsync(patientId)).ReturnsAsync(prescriptions);
        mapperMock.Setup(x => x.Map<IEnumerable<PrescriptionDto>>(prescriptions)).Returns(dtos);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeEquivalentTo(dtos);
    }
}
