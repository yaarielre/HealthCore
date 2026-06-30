using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Immunizations.Commands.Delete;

public class DeleteImmunizationCommandHandler : IRequestHandler<DeleteImmunizationCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteImmunizationCommandHandler> _logger;

    public DeleteImmunizationCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteImmunizationCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteImmunizationCommand request, CancellationToken cancellationToken)
    {
        var immunization = await _unitOfWork.Immunizations.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Inmunización con Id '{request.Id}' no encontrada.");

        _logger.LogInformation("Inmunización eliminada (soft): {Id} - {VaccineName} (dosis {DoseNumber})", immunization.Id, immunization.VaccineName, immunization.DoseNumber);
        immunization.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
