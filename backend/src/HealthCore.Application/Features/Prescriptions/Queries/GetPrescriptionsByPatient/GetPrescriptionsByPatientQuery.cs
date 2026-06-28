using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;

namespace HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionsByPatient;

public record GetPrescriptionsByPatientQuery(Guid PatientId) : IRequest<IEnumerable<PrescriptionDto>>;
