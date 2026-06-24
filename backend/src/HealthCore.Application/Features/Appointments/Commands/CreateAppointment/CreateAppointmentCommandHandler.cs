using AutoMapper;
using HealthCore.Application.Features.Appointment.Commands.CreateAppointment;
using HealthCore.Application.Features.Appointments.DTOs;
using HealthCore.Domain.Interfaces;
using HealthCore.Domain.Enums;
using AppointmentEntity = HealthCore.Domain.Entities.Appointment;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Commands.CreateAppointmentCommandHandler;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAppointmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var hasConflict = await _unitOfWork.Appointments.HasConflictAsync(
            request.Dto.DoctorId,
            request.Dto.AppointmentDate);

        if (hasConflict)
            throw new InvalidOperationException("El médico ya tiene una cita en ese horario.");

        var appointment = _mapper.Map<AppointmentEntity>(request.Dto);
        appointment.Id = Guid.NewGuid();
        appointment.Status = AppointmentStatus.Scheduled;

        await _unitOfWork.Appointments.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AppointmentDto>(appointment);
    }
}