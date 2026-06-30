using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.MedicalRecords.Commands.DeleteMedicalRecord;

public class DeleteMedicalRecordCommandHandler : IRequestHandler<DeleteMedicalRecordCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteMedicalRecordCommandHandler> _logger;

    public DeleteMedicalRecordCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteMedicalRecordCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        var record = await _unitOfWork.MedicalRecords.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Historial clínico con Id '{request.Id}' no encontrado.");

        _logger.LogInformation("Historial clínico eliminado (soft): {Id} - {RecordNumber}", record.Id, record.RecordNumber);
        record.IsActive = false;
        record.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
