using MediatR;
using HealthCore.Application.Features.Patients.DTOs;

namespace HealthCore.Application.Features.Patients.Queries.GetPatientAppointments;

public record GetPatientAppointmentsQuery(Guid PatientId) : IRequest<IEnumerable<MedicalHistoryAppointmentDto>>;
