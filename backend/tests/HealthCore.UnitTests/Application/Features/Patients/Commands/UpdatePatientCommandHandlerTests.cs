using AutoMapper;
using HealthCore.Application.Features.Patients.Commands.UpdatePatient;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.Patients.Commands;

public class UpdatePatientCommandHandlerTests
{
    private readonly Mock<IPatientRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdatePatientCommandHandler _handler;

    public UpdatePatientCommandHandlerTests()
    {
        _repositoryMock = new Mock<IPatientRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdatePatientCommandHandler(_repositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdatePatient_WhenValidRequest()
    {
        var patientId = Guid.NewGuid();
        var existingPatient = new Patient
        {
            Id = patientId,
            FirstName = "Old",
            LastName = "Name",
            Phone = "8095551234",
            Address = "Old Address"
        };
        var dto = new UpdatePatientDto
        {
            FirstName = "New",
            LastName = "Name",
            Phone = "8095551234",
            Address = "New Address"
        };
        var command = new UpdatePatientCommand(patientId, dto);
        var patientDto = new PatientDto { Id = patientId, FirstName = "New", LastName = "Name" };

        _repositoryMock.Setup(x => x.GetByIdAsync(patientId)).ReturnsAsync(existingPatient);
        _mapperMock.Setup(x => x.Map<PatientDto>(existingPatient)).Returns(patientDto);
        _mapperMock.Setup(x => x.Map(dto, existingPatient));

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        _repositoryMock.Verify(x => x.UpdateAsync(existingPatient), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenPatientNotFound()
    {
        var command = new UpdatePatientCommand(Guid.NewGuid(), new UpdatePatientDto());

        _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Patient?)null);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
