using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.OrderTypes.Commands.Delete;

public class DeleteOrderTypeCommandHandler : IRequestHandler<DeleteOrderTypeCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteOrderTypeCommandHandler> _logger;

    public DeleteOrderTypeCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteOrderTypeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteOrderTypeCommand request, CancellationToken cancellationToken)
    {
        var orderType = await _unitOfWork.OrderTypes.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Tipo de orden con Id '{request.Id}' no encontrado.");

        _logger.LogInformation("Tipo de orden desactivado: {Id} - {Name}", orderType.Id, orderType.Name);
        orderType.IsActive = false;
        orderType.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
