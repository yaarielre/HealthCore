using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Orders.Commands.Delete;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Orden con Id '{request.Id}' no encontrada.");

        _logger.LogInformation("Orden eliminada: {Id} - {OrderNumber}", order.Id, order.OrderNumber);

        await _unitOfWork.Orders.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
