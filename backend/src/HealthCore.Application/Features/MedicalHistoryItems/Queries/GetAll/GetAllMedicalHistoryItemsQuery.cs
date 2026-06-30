using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;

namespace HealthCore.Application.Features.MedicalHistoryItems.Queries.GetAll;

public record GetAllMedicalHistoryItemsQuery : IRequest<IEnumerable<MedicalHistoryItemDto>>;
