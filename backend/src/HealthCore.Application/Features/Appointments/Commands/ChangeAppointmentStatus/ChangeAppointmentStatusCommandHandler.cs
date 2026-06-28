using MediatR;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Appointments.Commands.ChangeAppointmentStatus;

public class ChangeAppointmentStatusCommandHandler : IRequestHandler<ChangeAppointmentStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeAppointmentStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ChangeAppointmentStatusCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Cita con Id '{request.Id}' no encontrada.");

        appointment.Status = request.NewStatus;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Appointments.UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}