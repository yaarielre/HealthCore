using HealthCore.API.Controllers;
using HealthCore.Application.Features.Prescriptions.Commands.CreatePrescription;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Application.Features.Prescriptions.Queries.GetAllPrescriptions;
using HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionById;
using HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionsByPatient;
using HealthCore.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.API.Controllers;

public class PrescriptionsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IPdfService> _pdfServiceMock;
    private readonly PrescriptionsController _controller;

    public PrescriptionsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _pdfServiceMock = new Mock<IPdfService>();
        _controller = new PrescriptionsController(_mediatorMock.Object, _pdfServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkWithList()
    {
        var dtos = new List<PrescriptionDto>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllPrescriptionsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dtos);

        var result = await _controller.GetAll();

        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(dtos);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenFound()
    {
        var dto = new PrescriptionDto(
            Guid.NewGuid(), Guid.NewGuid(), "P", Guid.NewGuid(), "D",
            Guid.NewGuid(), "Med", "Dose", "Freq", "Dur", null, DateTime.UtcNow);
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetPrescriptionByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto);

        var result = await _controller.GetById(dto.Id);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenNull()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetPrescriptionByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PrescriptionDto?)null);

        var result = await _controller.GetById(Guid.NewGuid());

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetByPatient_ShouldReturnOk()
    {
        var dtos = new List<PrescriptionDto>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetPrescriptionsByPatientQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dtos);

        var result = await _controller.GetByPatient(Guid.NewGuid());

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        var dto = new CreatePrescriptionDto(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Med", "Dose", "Freq", "Dur", null);
        var resultDto = new PrescriptionDto(
            Guid.NewGuid(), Guid.NewGuid(), "P", Guid.NewGuid(), "D",
            Guid.NewGuid(), "Med", "Dose", "Freq", "Dur", null, DateTime.UtcNow);
        _mediatorMock.Setup(x => x.Send(It.IsAny<CreatePrescriptionCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultDto);

        var result = await _controller.Create(dto);

        result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result as CreatedAtActionResult;
        createdResult!.ActionName.Should().Be(nameof(_controller.GetById));
        createdResult.RouteValues!["id"].Should().Be(resultDto.Id);
    }

    [Fact]
    public async Task GeneratePdf_ShouldReturnFile_WhenPrescriptionExists()
    {
        var prescriptionId = Guid.NewGuid();
        var dto = new PrescriptionDto(
            prescriptionId, Guid.NewGuid(), "Paciente Test", Guid.NewGuid(), "Doctor Test",
            Guid.NewGuid(), "Amoxicilina", "500mg", "Cada 8h", "7 días", "Tomar con alimentos", DateTime.UtcNow);
        var pdfBytes = new byte[] { 1, 2, 3 };

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetPrescriptionByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto);
        _pdfServiceMock.Setup(x => x.GeneratePrescriptionPdf(
            dto.PatientName, dto.DoctorName, dto.Medication, dto.Dosage,
            dto.Frequency, dto.Duration, dto.Instructions, dto.CreatedAt))
            .Returns(pdfBytes);

        var result = await _controller.GeneratePdf(prescriptionId);

        result.Should().BeOfType<FileContentResult>();
        var fileResult = result as FileContentResult;
        fileResult!.ContentType.Should().Be("application/pdf");
        fileResult.FileDownloadName.Should().Be($"receta_{prescriptionId}.pdf");
    }

    [Fact]
    public async Task GeneratePdf_ShouldReturnNotFound_WhenPrescriptionIsNull()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetPrescriptionByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PrescriptionDto?)null);

        var result = await _controller.GeneratePdf(Guid.NewGuid());

        result.Should().BeOfType<NotFoundResult>();
    }
}
