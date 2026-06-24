using HealthCore.Application.Features.Doctors.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Doctors.Queries.GetDoctorsBySpecialty;

public record GetDoctorsBySpecialtyQuery(Guid SpecialtyId) : IRequest<IEnumerable<DoctorDto>>;