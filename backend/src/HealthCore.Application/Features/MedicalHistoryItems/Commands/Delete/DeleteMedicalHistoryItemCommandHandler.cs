using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.MedicalHistoryItems.Commands.Delete;

public class DeleteMedicalHistoryItemCommandHandler : IRequestHandler<DeleteMedicalHistoryItemCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteMedicalHistoryItemCommandHandler> _logger;

    public DeleteMedicalHistoryItemCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteMedicalHistoryItemCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMedicalHistoryItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.MedicalHistoryItems.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Item de historial con Id '{request.Id}' no encontrado.");

        _logger.LogInformation("Item de historial eliminado (soft): {Id} - {Description}", item.Id, item.Description);
        item.IsActive = false;
        item.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
