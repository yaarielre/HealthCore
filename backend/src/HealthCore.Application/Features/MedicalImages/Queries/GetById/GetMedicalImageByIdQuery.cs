using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;

namespace HealthCore.Application.Features.MedicalImages.Queries.GetById;

public record GetMedicalImageByIdQuery(Guid Id) : IRequest<MedicalImageDto?>;
