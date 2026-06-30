using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;

namespace HealthCore.Application.Features.MedicalHistoryItems.Commands.Create;

public record CreateMedicalHistoryItemCommand(CreateMedicalHistoryItemDto Dto) : IRequest<MedicalHistoryItemDto>;
