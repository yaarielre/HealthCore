using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;

namespace HealthCore.Application.Features.MedicalImages.Commands.Create;

public record CreateMedicalImageCommand(CreateMedicalImageDto Dto) : IRequest<MedicalImageDto>;
