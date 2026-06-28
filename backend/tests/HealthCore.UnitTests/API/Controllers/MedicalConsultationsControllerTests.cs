using HealthCore.API.Controllers;
using HealthCore.Application.Features.MedicalConsultations.Commands.CreateMedicalConsultation;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Application.Features.MedicalConsultations.Queries.GetAllMedicalConsultations;
using HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationById;
using HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationsByPatient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.API.Controllers;

public class MedicalConsultationsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly MedicalConsultationsController _controller;

    public MedicalConsultationsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new MedicalConsultationsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkWithList()
    {
        var dtos = new List<MedicalConsultationDto>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllMedicalConsultationsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dtos);

        var result = await _controller.GetAll();

        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(dtos);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenFound()
    {
        var dto = new MedicalConsultationDto(
            Guid.NewGuid(), Guid.NewGuid(), "P", Guid.NewGuid(), "D", null,
            "R", null, null, null, null, null, new List<VitalSignDto>(), DateTime.UtcNow);
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetMedicalConsultationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto);

        var result = await _controller.GetById(dto.Id);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenNull()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetMedicalConsultationByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((MedicalConsultationDto?)null);

        var result = await _controller.GetById(Guid.NewGuid());

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetByPatient_ShouldReturnOk()
    {
        var dtos = new List<MedicalConsultationDto>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetMedicalConsultationsByPatientQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dtos);

        var result = await _controller.GetByPatient(Guid.NewGuid());

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        var dto = new CreateMedicalConsultationDto(
            Guid.NewGuid(), Guid.NewGuid(), null, "Reason", null, null, null, null, null, null);
        var resultDto = new MedicalConsultationDto(
            Guid.NewGuid(), Guid.NewGuid(), "P", Guid.NewGuid(), "D", null,
            "Reason", null, null, null, null, null, new List<VitalSignDto>(), DateTime.UtcNow);
        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateMedicalConsultationCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultDto);

        var result = await _controller.Create(dto);

        result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result as CreatedAtActionResult;
        createdResult!.ActionName.Should().Be(nameof(_controller.GetById));
        createdResult.RouteValues!["id"].Should().Be(resultDto.Id);
    }
}
