using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.OrderItems.Commands.Delete;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteOrderItemCommandHandler> _logger;

    public DeleteOrderItemCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteOrderItemCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.OrderItems.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Item de orden con Id '{request.Id}' no encontrado.");

        _logger.LogInformation("Item de orden eliminado: {Id} - {ItemName}", item.Id, item.ItemName);

        await _unitOfWork.OrderItems.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
