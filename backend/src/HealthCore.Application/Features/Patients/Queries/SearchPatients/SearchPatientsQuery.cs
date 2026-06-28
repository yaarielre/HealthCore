using MediatR;
using HealthCore.Application.Features.Patients.DTOs;

namespace HealthCore.Application.Features.Patients.Queries.SearchPatients;

public record SearchPatientsQuery(string Term, int Page = 1, int PageSize = 10) : IRequest<IEnumerable<PatientSearchDto>>;
