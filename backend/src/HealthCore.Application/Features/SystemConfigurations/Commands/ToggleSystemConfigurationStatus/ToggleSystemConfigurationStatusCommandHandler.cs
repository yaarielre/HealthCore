using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.SystemConfigurations.Commands.ToggleSystemConfigurationStatus;

public class ToggleSystemConfigurationStatusCommandHandler : IRequestHandler<ToggleSystemConfigurationStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ToggleSystemConfigurationStatusCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(ToggleSystemConfigurationStatusCommand request, CancellationToken cancellationToken)
    {
        var config = await _unitOfWork.SystemConfigurations.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Configuración con Id {request.Id} no encontrada.");

        config.IsActive = !config.IsActive;
        await _unitOfWork.SystemConfigurations.UpdateAsync(config);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return config.IsActive;
    }
}
