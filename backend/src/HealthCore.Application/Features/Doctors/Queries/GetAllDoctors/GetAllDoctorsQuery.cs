using HealthCore.Application.Features.Doctors.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Doctors.Queries.GetAllDoctors;

public record GetAllDoctorsQuery : IRequest<IEnumerable<DoctorDto>>;