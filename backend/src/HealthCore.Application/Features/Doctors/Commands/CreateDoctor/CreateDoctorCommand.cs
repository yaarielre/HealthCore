using HealthCore.Application.Features.Doctors.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Doctors.Commands.CreateDoctor;

public record CreateDoctorCommand(
    string FirstName,
    string LastName,
    string LicenseNumber,
    Guid SpecialtyId) : IRequest<DoctorDto>;