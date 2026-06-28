using AutoMapper;
using HealthCore.Application.Features.Patients.Commands.CreatePatient;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.Patients.Commands;

public class CreatePatientCommandHandlerTests
{
    private readonly Mock<IPatientRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CreatePatientCommandHandler>> _loggerMock;
    private readonly CreatePatientCommandHandler _handler;

    public CreatePatientCommandHandlerTests()
    {
        _repositoryMock = new Mock<IPatientRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CreatePatientCommandHandler>>();
        _handler = new CreatePatientCommandHandler(
            _repositoryMock.Object, _unitOfWorkMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreatePatient_WhenValidRequest()
    {
        var dto = new CreatePatientDto
        {
            FirstName = "Juan",
            LastName = "Perez",
            IdNumber = "00123456789",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "8095551234",
            Address = "Calle Principal"
        };
        var command = new CreatePatientCommand(dto);
        var patient = new Patient { Id = Guid.NewGuid(), FirstName = "Juan", LastName = "Perez" };
        var patientDto = new PatientDto { Id = patient.Id, FirstName = "Juan", LastName = "Perez", FullName = "Juan Perez" };

        _repositoryMock.Setup(x => x.ExistsByIdNumberAsync(dto.IdNumber, null)).ReturnsAsync(false);
        _mapperMock.Setup(x => x.Map<Patient>(dto)).Returns(patient);
        _mapperMock.Setup(x => x.Map<PatientDto>(patient)).Returns(patientDto);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.FullName.Should().Be("Juan Perez");
        _repositoryMock.Verify(x => x.AddAsync(patient), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenIdNumberAlreadyExists()
    {
        var dto = new CreatePatientDto
        {
            FirstName = "Juan",
            LastName = "Perez",
            IdNumber = "00123456789",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "8095551234",
            Address = "Calle Principal"
        };
        var command = new CreatePatientCommand(dto);

        _repositoryMock.Setup(x => x.ExistsByIdNumberAsync(dto.IdNumber, null)).ReturnsAsync(true);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"*{dto.IdNumber}*");
    }
}
