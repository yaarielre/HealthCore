using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.MedicalImages.Commands.Delete;

public class DeleteMedicalImageCommandHandler : IRequestHandler<DeleteMedicalImageCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteMedicalImageCommandHandler> _logger;

    public DeleteMedicalImageCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteMedicalImageCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMedicalImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _unitOfWork.MedicalImages.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Imagen médica con Id '{request.Id}' no encontrada.");

        _logger.LogInformation("Imagen médica eliminada (soft): {Id} - {FileName}", image.Id, image.FileName);
        image.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
