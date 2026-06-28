using HealthCore.Application.Features.Patients.Commands.DeletePatient;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.Patients.Commands;

public class DeletePatientCommandHandlerTests
{
    private readonly Mock<IPatientRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<DeletePatientCommandHandler>> _loggerMock;
    private readonly DeletePatientCommandHandler _handler;

    public DeletePatientCommandHandlerTests()
    {
        _repositoryMock = new Mock<IPatientRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<DeletePatientCommandHandler>>();
        _handler = new DeletePatientCommandHandler(_repositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldSoftDeletePatient()
    {
        var patientId = Guid.NewGuid();
        var patient = new Patient { Id = patientId, IsDeleted = false, IsActive = true };

        _repositoryMock.Setup(x => x.GetByIdAsync(patientId)).ReturnsAsync(patient);

        var result = await _handler.Handle(new DeletePatientCommand(patientId), CancellationToken.None);

        result.Should().BeTrue();
        patient.IsDeleted.Should().BeTrue();
        patient.IsActive.Should().BeFalse();
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenPatientNotFound()
    {
        _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Patient?)null);

        var act = () => _handler.Handle(new DeletePatientCommand(Guid.NewGuid()), CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
