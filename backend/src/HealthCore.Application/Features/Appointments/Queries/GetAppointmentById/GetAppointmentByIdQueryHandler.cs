using AutoMapper;
using HealthCore.Application.Features.Appointments.DTOs;
using HealthCore.Application.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Cita con Id '{request.Id}' no encontrada.");

        return _mapper.Map<AppointmentDto>(appointment);
    }
}
