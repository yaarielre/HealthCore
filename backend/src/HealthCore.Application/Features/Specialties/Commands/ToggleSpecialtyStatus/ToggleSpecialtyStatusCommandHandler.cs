using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Specialties.Commands.ToggleSpecialtyStatus;

public class ToggleSpecialtyStatusCommandHandler : IRequestHandler<ToggleSpecialtyStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ToggleSpecialtyStatusCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(ToggleSpecialtyStatusCommand request, CancellationToken cancellationToken)
    {
        var specialty = await _unitOfWork.Specialties.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Especialidad con Id {request.Id} no encontrada.");

        specialty.IsActive = !specialty.IsActive;
        await _unitOfWork.Specialties.UpdateAsync(specialty);
        await _unitOfWork.SaveChangesAsync();

        return specialty.IsActive;
    }
}