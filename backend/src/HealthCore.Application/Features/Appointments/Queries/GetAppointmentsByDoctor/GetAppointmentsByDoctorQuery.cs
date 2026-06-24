using HealthCore.Application.Features.Appointments.DTOs;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Queries.GetAppointmentsByDoctor;
public record GetAppointmentsByDoctorQuery(Guid DoctorId) : IRequest<IEnumerable<AppointmentDto>>;