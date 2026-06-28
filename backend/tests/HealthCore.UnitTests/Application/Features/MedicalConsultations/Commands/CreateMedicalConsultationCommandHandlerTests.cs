using AutoMapper;
using HealthCore.Application.Features.MedicalConsultations.Commands.CreateMedicalConsultation;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.MedicalConsultations.Commands;

public class CreateMedicalConsultationCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateMedicalConsultationCommandHandler _handler;
    private readonly Guid _doctorId = Guid.NewGuid();
    private readonly Guid _patientId = Guid.NewGuid();

    public CreateMedicalConsultationCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateMedicalConsultationCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateConsultation_WhenValidRequest()
    {
        var doctor = new Doctor { Id = _doctorId, FirstName = "Juan", LastName = "Perez" };
        var patient = new Patient { Id = _patientId, FirstName = "Maria", LastName = "Gomez" };
        var dto = new CreateMedicalConsultationDto(
            _patientId, _doctorId, null, "Dolor de cabeza", null, null, null, null, null,
            new CreateVitalSignDto("120/80", 37.5m, 75m, 170m, 80, 98));
        var command = new CreateMedicalConsultationCommand(dto);
        var consultationDto = new MedicalConsultationDto(
            Guid.NewGuid(), _patientId, "Maria Gomez", _doctorId, "Juan Perez", null,
            "Dolor de cabeza", null, null, null, null, null, new List<VitalSignDto>(), DateTime.UtcNow);

        _unitOfWorkMock.Setup(x => x.Doctors.GetByIdAsync(_doctorId)).ReturnsAsync(doctor);
        _unitOfWorkMock.Setup(x => x.Patients.GetByIdAsync(_patientId)).ReturnsAsync(patient);
        _unitOfWorkMock.Setup(x => x.MedicalConsultations.AddAsync(It.IsAny<MedicalConsultation>()))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _mapperMock.Setup(x => x.Map<MedicalConsultationDto>(It.IsAny<MedicalConsultation>()))
            .Returns(consultationDto);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.PatientName.Should().Be("Maria Gomez");
        result.DoctorName.Should().Be("Juan Perez");
        _unitOfWorkMock.Verify(x => x.MedicalConsultations.AddAsync(It.IsAny<MedicalConsultation>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenDoctorNotFound()
    {
        var dto = new CreateMedicalConsultationDto(
            _patientId, _doctorId, null, "Dolor", null, null, null, null, null, null);
        var command = new CreateMedicalConsultationCommand(dto);

        _unitOfWorkMock.Setup(x => x.Doctors.GetByIdAsync(_doctorId)).ReturnsAsync((Doctor?)null);
        _unitOfWorkMock.Setup(x => x.Patients.GetByIdAsync(_patientId)).ReturnsAsync(new Patient());

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"*'{_doctorId}'*");
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenPatientNotFound()
    {
        var dto = new CreateMedicalConsultationDto(
            _patientId, _doctorId, null, "Dolor", null, null, null, null, null, null);
        var command = new CreateMedicalConsultationCommand(dto);

        _unitOfWorkMock.Setup(x => x.Doctors.GetByIdAsync(_doctorId)).ReturnsAsync(new Doctor());
        _unitOfWorkMock.Setup(x => x.Patients.GetByIdAsync(_patientId)).ReturnsAsync((Patient?)null);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"*'{_patientId}'*");
    }

    [Fact]
    public async Task Handle_ShouldCreateConsultationWithoutVitalSigns_WhenVitalSignsNull()
    {
        var doctor = new Doctor { Id = _doctorId, FirstName = "Juan", LastName = "Perez" };
        var patient = new Patient { Id = _patientId, FirstName = "Maria", LastName = "Gomez" };
        var dto = new CreateMedicalConsultationDto(
            _patientId, _doctorId, null, "Dolor", null, null, null, null, null, null);
        var command = new CreateMedicalConsultationCommand(dto);

        _unitOfWorkMock.Setup(x => x.Doctors.GetByIdAsync(_doctorId)).ReturnsAsync(doctor);
        _unitOfWorkMock.Setup(x => x.Patients.GetByIdAsync(_patientId)).ReturnsAsync(patient);
        _unitOfWorkMock.Setup(x => x.MedicalConsultations.AddAsync(It.IsAny<MedicalConsultation>()))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _mapperMock.Setup(x => x.Map<MedicalConsultationDto>(It.IsAny<MedicalConsultation>()))
            .Returns(new MedicalConsultationDto(
                Guid.NewGuid(), _patientId, "Maria Gomez", _doctorId, "Juan Perez", null,
                "Dolor", null, null, null, null, null, new List<VitalSignDto>(), DateTime.UtcNow));

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
    }
}
