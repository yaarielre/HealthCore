using AutoMapper;
using HealthCore.Application.Features.Prescriptions.Commands.CreatePrescription;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.Prescriptions.Commands;

public class CreatePrescriptionCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreatePrescriptionCommandHandler _handler;
    private readonly Guid _doctorId = Guid.NewGuid();
    private readonly Guid _patientId = Guid.NewGuid();
    private readonly Guid _consultationId = Guid.NewGuid();

    public CreatePrescriptionCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreatePrescriptionCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreatePrescription_WhenValidRequest()
    {
        var doctor = new Doctor { Id = _doctorId, FirstName = "Juan", LastName = "Perez" };
        var patient = new Patient { Id = _patientId, FirstName = "Maria", LastName = "Gomez" };
        var dto = new CreatePrescriptionDto(
            _patientId, _doctorId, _consultationId,
            "Amoxicilina", "500mg", "Cada 8 horas", "7 días", "Tomar con alimentos");
        var command = new CreatePrescriptionCommand(dto);
        var prescriptionDto = new PrescriptionDto(
            Guid.NewGuid(), _patientId, "Maria Gomez", _doctorId, "Juan Perez", _consultationId,
            "Amoxicilina", "500mg", "Cada 8 horas", "7 días", "Tomar con alimentos", DateTime.UtcNow);

        _unitOfWorkMock.Setup(x => x.Doctors.GetByIdAsync(_doctorId)).ReturnsAsync(doctor);
        _unitOfWorkMock.Setup(x => x.Patients.GetByIdAsync(_patientId)).ReturnsAsync(patient);
        _unitOfWorkMock.Setup(x => x.Prescriptions.AddAsync(It.IsAny<Prescription>()))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _mapperMock.Setup(x => x.Map<PrescriptionDto>(It.IsAny<Prescription>())).Returns(prescriptionDto);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.PatientName.Should().Be("Maria Gomez");
        result.DoctorName.Should().Be("Juan Perez");
        result.Medication.Should().Be("Amoxicilina");
        _unitOfWorkMock.Verify(x => x.Prescriptions.AddAsync(It.IsAny<Prescription>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenDoctorNotFound()
    {
        var dto = new CreatePrescriptionDto(
            _patientId, _doctorId, _consultationId, "Meds", "500mg", "8h", "7d", null);
        var command = new CreatePrescriptionCommand(dto);

        _unitOfWorkMock.Setup(x => x.Doctors.GetByIdAsync(_doctorId)).ReturnsAsync((Doctor?)null);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenPatientNotFound()
    {
        var dto = new CreatePrescriptionDto(
            _patientId, _doctorId, _consultationId, "Meds", "500mg", "8h", "7d", null);
        var command = new CreatePrescriptionCommand(dto);

        _unitOfWorkMock.Setup(x => x.Doctors.GetByIdAsync(_doctorId)).ReturnsAsync(new Doctor());
        _unitOfWorkMock.Setup(x => x.Patients.GetByIdAsync(_patientId)).ReturnsAsync((Patient?)null);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
