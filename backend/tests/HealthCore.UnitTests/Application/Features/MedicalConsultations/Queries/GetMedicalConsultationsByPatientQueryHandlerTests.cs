using AutoMapper;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationsByPatient;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.MedicalConsultations.Queries;

public class GetMedicalConsultationsByPatientQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnConsultationsForPatient()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetMedicalConsultationsByPatientQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
        var patientId = Guid.NewGuid();
        var query = new GetMedicalConsultationsByPatientQuery(patientId);
        var consultations = new List<MedicalConsultation> { new() { Id = Guid.NewGuid(), PatientId = patientId } };
        var dtos = new List<MedicalConsultationDto>
        {
            new(Guid.NewGuid(), patientId, "P", Guid.NewGuid(), "D", null,
                "R", null, null, null, null, null, new List<VitalSignDto>(), DateTime.UtcNow)
        };

        unitOfWorkMock.Setup(x => x.MedicalConsultations.GetByPatientIdAsync(patientId)).ReturnsAsync(consultations);
        mapperMock.Setup(x => x.Map<IEnumerable<MedicalConsultationDto>>(consultations)).Returns(dtos);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeEquivalentTo(dtos);
    }
}
