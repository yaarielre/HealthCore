using MediatR;
using HealthCore.Application.Features.Patients.DTOs;

namespace HealthCore.Application.Features.Patients.Queries.GetAllPatients;

public record GetAllPatientsQuery : IRequest<IEnumerable<PatientDto>>;