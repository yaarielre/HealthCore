using MediatR;
using HealthCore.Application.Features.Patients.DTOs;

namespace HealthCore.Application.Features.Patients.Queries.GetPatientByIdNumber;

public record GetPatientByIdNumberQuery(string IdNumber) : IRequest<PatientDto>;
