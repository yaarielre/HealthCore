using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;

namespace HealthCore.Application.Features.MedicalImages.Commands.Update;

public record UpdateMedicalImageCommand(Guid Id, UpdateMedicalImageDto Dto) : IRequest<MedicalImageDto>;
