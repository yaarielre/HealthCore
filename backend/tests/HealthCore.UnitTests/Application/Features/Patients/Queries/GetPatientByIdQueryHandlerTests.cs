using AutoMapper;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Application.Features.Patients.Queries.GetPatientById;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.Patients.Queries;

public class GetPatientByIdQueryHandlerTests
{
    private readonly Mock<IPatientRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetPatientByIdQueryHandler _handler;

    public GetPatientByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IPatientRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetPatientByIdQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnDto_WhenPatientExists()
    {
        var patientId = Guid.NewGuid();
        var patient = new Patient { Id = patientId };
        var dto = new PatientDto { Id = patientId, FirstName = "Juan" };

        _repositoryMock.Setup(x => x.GetByIdAsync(patientId)).ReturnsAsync(patient);
        _mapperMock.Setup(x => x.Map<PatientDto>(patient)).Returns(dto);

        var result = await _handler.Handle(new GetPatientByIdQuery(patientId), CancellationToken.None);

        result?.Id.Should().Be(patientId);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenPatientNotFound()
    {
        _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Patient?)null);

        var act = () => _handler.Handle(new GetPatientByIdQuery(Guid.NewGuid()), CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
