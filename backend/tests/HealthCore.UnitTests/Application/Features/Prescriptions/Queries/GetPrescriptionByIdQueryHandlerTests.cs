using AutoMapper;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionById;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.Prescriptions.Queries;

public class GetPrescriptionByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetPrescriptionByIdQueryHandler _handler;

    public GetPrescriptionByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetPrescriptionByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnDto_WhenPrescriptionExists()
    {
        var prescriptionId = Guid.NewGuid();
        var query = new GetPrescriptionByIdQuery(prescriptionId);
        var prescription = new Prescription { Id = prescriptionId };
        var dto = new PrescriptionDto(
            prescriptionId, Guid.NewGuid(), "Patient", Guid.NewGuid(), "Doctor",
            Guid.NewGuid(), "Med", "Dose", "Freq", "Dur", null, DateTime.UtcNow);

        _unitOfWorkMock.Setup(x => x.Prescriptions.GetByIdAsync(prescriptionId)).ReturnsAsync(prescription);
        _mapperMock.Setup(x => x.Map<PrescriptionDto>(prescription)).Returns(dto);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().Be(dto);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenPrescriptionNotFound()
    {
        var query = new GetPrescriptionByIdQuery(Guid.NewGuid());

        _unitOfWorkMock.Setup(x => x.Prescriptions.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Prescription?)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }
}
