using HealthCore.Application.Features.Specialties.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Specialties.Queries.GetAllSpecialties;

public record GetAllSpecialtiesQuery : IRequest<IEnumerable<SpecialtyDto>>;