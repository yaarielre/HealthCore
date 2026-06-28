using AutoMapper;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Application.Features.Patients.Queries.SearchPatients;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using Moq;
using FluentAssertions;

namespace HealthCore.UnitTests.Application.Features.Patients.Queries;

public class SearchPatientsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedResults()
    {
        var repositoryMock = new Mock<IPatientRepository>();
        var mapperMock = new Mock<IMapper>();
        var handler = new SearchPatientsQueryHandler(repositoryMock.Object, mapperMock.Object);
        var patients = new List<Patient> { new() { Id = Guid.NewGuid(), FirstName = "Juan" } };
        var dtos = new List<PatientSearchDto>
        {
            new() { Id = patients[0].Id, FullName = "Juan Perez", IdNumber = "001" }
        };

        repositoryMock.Setup(x => x.SearchAsync("Juan")).ReturnsAsync(patients);
        mapperMock.Setup(x => x.Map<IEnumerable<PatientSearchDto>>(patients)).Returns(dtos);

        var result = await handler.Handle(new SearchPatientsQuery("Juan"), CancellationToken.None);

        result.Should().BeEquivalentTo(dtos);
    }
}
