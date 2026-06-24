using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Doctors.Commands.ToggleDoctorStatus;

public class ToggleDoctorStatusCommandHandler : IRequestHandler<ToggleDoctorStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ToggleDoctorStatusCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(ToggleDoctorStatusCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Doctor con Id {request.Id} no encontrado.");

        doctor.IsActive = !doctor.IsActive;
        await _unitOfWork.Doctors.UpdateAsync(doctor);
        await _unitOfWork.SaveChangesAsync();

        return doctor.IsActive;
    }
}