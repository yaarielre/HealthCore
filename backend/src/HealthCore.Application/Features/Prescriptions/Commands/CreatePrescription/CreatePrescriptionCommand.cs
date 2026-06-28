using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;

namespace HealthCore.Application.Features.Prescriptions.Commands.CreatePrescription;

public record CreatePrescriptionCommand(CreatePrescriptionDto Dto) : IRequest<PrescriptionDto>;
