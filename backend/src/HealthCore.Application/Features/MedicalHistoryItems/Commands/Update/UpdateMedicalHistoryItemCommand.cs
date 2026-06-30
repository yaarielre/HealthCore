using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;

namespace HealthCore.Application.Features.MedicalHistoryItems.Commands.Update;

public record UpdateMedicalHistoryItemCommand(Guid Id, UpdateMedicalHistoryItemDto Dto) : IRequest<MedicalHistoryItemDto>;
