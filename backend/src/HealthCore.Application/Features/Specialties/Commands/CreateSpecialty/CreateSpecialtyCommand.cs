
using HealthCore.Application.Features.Specialties.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Specialties.Commands.CreateSpecialty;
public record CreateSpecialtyCommand(string Name, string? Description) : IRequest<SpecialtyDto>;

