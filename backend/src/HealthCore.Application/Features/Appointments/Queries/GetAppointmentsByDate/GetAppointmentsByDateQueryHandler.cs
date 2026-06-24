using AutoMapper;
using HealthCore.Application.Features.Appointments.DTOs;
using HealthCore.Application.Features.Appointment.Queries.GetAppointmentsByDate;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Queries.GetAppointmentsByDate;

public class GetAppointmentsByDateQueryHandler : IRequestHandler<GetAppointmentsByDateQuery, IEnumerable<AppointmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAppointmentsByDateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetAppointmentsByDateQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _unitOfWork.Appointments.GetByDateAsync(request.Date);
        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }
}