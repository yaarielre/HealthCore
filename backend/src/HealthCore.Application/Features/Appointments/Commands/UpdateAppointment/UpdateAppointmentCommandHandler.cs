using AutoMapper;
using HealthCore.Application.Features.Appointments.Commands.UpdateAppointment;
using HealthCore.Application.Features.Appointments.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Commands.UpdateAppointment;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, AppointmentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAppointmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AppointmentDto> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Cita con Id '{request.Id}' no encontrada.");

        var hasConflict = await _unitOfWork.Appointments.HasConflictAsync(
            appointment.DoctorId,
            request.Dto.AppointmentDate,
            excludeId: request.Id);

        if (hasConflict)
            throw new InvalidOperationException("El médico ya tiene una cita en ese horario.");

        appointment.AppointmentDate = request.Dto.AppointmentDate;
        appointment.Reason = request.Dto.Reason;
        appointment.Notes = request.Dto.Notes;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Appointments.UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AppointmentDto>(appointment);
    }
}