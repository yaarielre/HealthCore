using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;

namespace HealthCore.Application.Features.MedicalHistoryItems.Queries.GetById;

public record GetMedicalHistoryItemByIdQuery(Guid Id) : IRequest<MedicalHistoryItemDto?>;
