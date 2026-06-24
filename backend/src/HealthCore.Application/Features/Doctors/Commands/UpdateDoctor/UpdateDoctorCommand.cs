using HealthCore.Application.Features.Doctors.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Doctors.Commands.UpdateDoctor;

public record UpdateDoctorCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string LicenseNumber,
    Guid SpecialtyId) : IRequest<DoctorDto>;