using HealthCore.Application.Features.Doctors.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Doctors.Queries.GetDoctorById;

public record GetDoctorByIdQuery(Guid Id) : IRequest<DoctorDto>;