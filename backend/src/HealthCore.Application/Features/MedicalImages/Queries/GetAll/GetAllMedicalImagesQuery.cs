using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;

namespace HealthCore.Application.Features.MedicalImages.Queries.GetAll;

public record GetAllMedicalImagesQuery : IRequest<IEnumerable<MedicalImageDto>>;
