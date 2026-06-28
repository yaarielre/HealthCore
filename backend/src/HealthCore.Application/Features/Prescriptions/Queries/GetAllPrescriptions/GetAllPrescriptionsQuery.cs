using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;

namespace HealthCore.Application.Features.Prescriptions.Queries.GetAllPrescriptions;

public record GetAllPrescriptionsQuery : IRequest<IEnumerable<PrescriptionDto>>;
