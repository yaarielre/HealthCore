using AutoMapper;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Application.Features.Prescriptions.Queries.GetAllPrescriptions;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.Prescriptions.Queries;

public class GetAllPrescriptionsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedPrescriptions()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var mapperMock = new Mock<IMapper>();
        var handler = new GetAllPrescriptionsQueryHandler(unitOfWorkMock.Object, mapperMock.Object);
        var prescriptions = new List<Prescription> { new() { Id = Guid.NewGuid() } };
        var dtos = new List<PrescriptionDto>
        {
            new(Guid.NewGuid(), Guid.NewGuid(), "P", Guid.NewGuid(), "D",
                Guid.NewGuid(), "Med", "Dose", "Freq", "Dur", null, DateTime.UtcNow)
        };

        unitOfWorkMock.Setup(x => x.Prescriptions.GetAllAsync()).ReturnsAsync(prescriptions);
        mapperMock.Setup(x => x.Map<IEnumerable<PrescriptionDto>>(prescriptions)).Returns(dtos);

        var result = await handler.Handle(new GetAllPrescriptionsQuery(), CancellationToken.None);

        result.Should().BeEquivalentTo(dtos);
    }
}
