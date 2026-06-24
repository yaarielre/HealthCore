using HealthCore.Application.Features.Specialties.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Specialties.Queries.GetSpecialtyById;

public record GetSpecialtyByIdQuery(Guid Id) : IRequest<SpecialtyDto>;