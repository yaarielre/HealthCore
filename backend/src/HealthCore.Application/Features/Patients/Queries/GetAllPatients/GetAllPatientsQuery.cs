using MediatR;
using HealthCore.Application.Features.Patients.DTOs;

namespace HealthCore.Application.Features.Patients.Queries.GetAllPatients;

public record GetAllPatientsQuery(int Page = 1, int PageSize = 20) : IRequest<IEnumerable<PatientDto>>;