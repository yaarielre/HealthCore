using AutoMapper;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationById;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.MedicalConsultations.Queries;

public class GetMedicalConsultationByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetMedicalConsultationByIdQueryHandler _handler;

    public GetMedicalConsultationByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetMedicalConsultationByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnDto_WhenConsultationExists()
    {
        var consultationId = Guid.NewGuid();
        var query = new GetMedicalConsultationByIdQuery(consultationId);
        var consultation = new MedicalConsultation { Id = consultationId };
        var dto = new MedicalConsultationDto(
            consultationId, Guid.NewGuid(), "Patient", Guid.NewGuid(), "Doctor", null,
            "Reason", null, null, null, null, null, new List<VitalSignDto>(), DateTime.UtcNow);

        _unitOfWorkMock.Setup(x => x.MedicalConsultations.GetWithDetailsAsync(consultationId))
            .ReturnsAsync(consultation);
        _mapperMock.Setup(x => x.Map<MedicalConsultationDto>(consultation)).Returns(dto);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().Be(dto);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenConsultationNotFound()
    {
        var query = new GetMedicalConsultationByIdQuery(Guid.NewGuid());

        _unitOfWorkMock.Setup(x => x.MedicalConsultations.GetWithDetailsAsync(It.IsAny<Guid>()))
            .ReturnsAsync((MedicalConsultation?)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
    }
}
