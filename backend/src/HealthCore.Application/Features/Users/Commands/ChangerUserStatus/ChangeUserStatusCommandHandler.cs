using MediatR;
using HealthCore.Application.Interfaces;

namespace HealthCore.Application.Features.Users.Commands.ChangeUserStatus;

public class ChangeUserStatusCommandHandler : IRequestHandler<ChangeUserStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Usuario con Id '{request.Id}' no encontrado.");

        user.Status = request.NewStatus;
        user.UpdateAt = DateTime.UtcNow;

        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}