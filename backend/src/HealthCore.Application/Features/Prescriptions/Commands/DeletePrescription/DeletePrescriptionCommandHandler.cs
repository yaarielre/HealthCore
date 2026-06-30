using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Prescriptions.Commands.DeletePrescription;

public class DeletePrescriptionCommandHandler : IRequestHandler<DeletePrescriptionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeletePrescriptionCommandHandler> _logger;

    public DeletePrescriptionCommandHandler(IUnitOfWork unitOfWork, ILogger<DeletePrescriptionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Receta con Id '{request.Id}' no encontrada.");

        _logger.LogInformation("Receta eliminada: {Id}", prescription.Id);

        await _unitOfWork.Prescriptions.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
