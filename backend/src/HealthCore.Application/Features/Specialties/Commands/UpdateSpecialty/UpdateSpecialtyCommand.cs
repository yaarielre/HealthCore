using HealthCore.Application.Features.Specialties.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Specialties.Commands.UpdateSpecialty;
public record UpdateSpecialtyCommand(Guid Id, string Name, string? Description) : IRequest<SpecialtyDto>;
