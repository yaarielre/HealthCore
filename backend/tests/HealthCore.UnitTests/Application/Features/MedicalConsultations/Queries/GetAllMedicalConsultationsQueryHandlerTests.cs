using AutoMapper;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Application.Features.MedicalConsultations.Queries.GetAllMedicalConsultations;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.MedicalConsultations.Queries;

public class GetAllMedicalConsultationsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedConsultations()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetAllMedicalConsultationsQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
        var consultations = new List<MedicalConsultation> { new() { Id = Guid.NewGuid() } };
        var dtos = new List<MedicalConsultationDto>
        {
            new(Guid.NewGuid(), Guid.NewGuid(), "P", Guid.NewGuid(), "D", null,
                "R", null, null, null, null, null, new List<VitalSignDto>(), DateTime.UtcNow)
        };

        unitOfWorkMock.Setup(x => x.MedicalConsultations.GetAllAsync()).ReturnsAsync(consultations);
        mapperMock.Setup(x => x.Map<IEnumerable<MedicalConsultationDto>>(consultations)).Returns(dtos);

        var result = await handler.Handle(new GetAllMedicalConsultationsQuery(), CancellationToken.None);

        result.Should().BeEquivalentTo(dtos);
    }
}
