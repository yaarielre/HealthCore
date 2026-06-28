using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;

namespace HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionById;

public record GetPrescriptionByIdQuery(Guid Id) : IRequest<PrescriptionDto?>;
