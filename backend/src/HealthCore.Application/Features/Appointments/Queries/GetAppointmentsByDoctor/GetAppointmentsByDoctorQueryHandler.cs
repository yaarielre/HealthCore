using AutoMapper;
using HealthCore.Application.Features.Appointments.DTOs;
using HealthCore.Application.Features.Appointment.Queries.GetAppointmentsByDoctor;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Appointments.Queries.GetAppointmentsByDoctor;

public class GetAppointmentsByDoctorQueryHandler : IRequestHandler<GetAppointmentsByDoctorQuery, IEnumerable<AppointmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAppointmentsByDoctorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetAppointmentsByDoctorQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _unitOfWork.Appointments.GetByDoctorIdAsync(request.DoctorId);
        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }
}