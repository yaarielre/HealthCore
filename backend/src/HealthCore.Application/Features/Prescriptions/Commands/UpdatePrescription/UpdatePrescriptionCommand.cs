using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;

namespace HealthCore.Application.Features.Prescriptions.Commands.UpdatePrescription;

public record UpdatePrescriptionCommand(Guid Id, UpdatePrescriptionDto Dto) : IRequest<PrescriptionDto>;
