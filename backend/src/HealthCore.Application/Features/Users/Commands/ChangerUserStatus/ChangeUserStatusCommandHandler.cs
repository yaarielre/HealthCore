using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Users.Commands.ChangeUserStatus;

public class ChangeUserStatusCommandHandler : IRequestHandler<ChangeUserStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ChangeUserStatusCommandHandler> _logger;

    public ChangeUserStatusCommandHandler(IUnitOfWork unitOfWork, ILogger<ChangeUserStatusCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Usuario con Id '{request.Id}' no encontrado.");

        user.Status = request.NewStatus;
        user.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Estado cambiado para usuario {UserId}: {NewStatus}", request.Id, request.NewStatus);
        return true;
    }
}