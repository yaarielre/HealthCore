using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Patients.Queries.GetPatientAppointments;

public class GetPatientAppointmentsQueryHandler : IRequestHandler<GetPatientAppointmentsQuery, IEnumerable<MedicalHistoryAppointmentDto>>
{
    private readonly IAppointmentRepository _repository;
    private readonly IMapper _mapper;

    public GetPatientAppointmentsQueryHandler(IAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicalHistoryAppointmentDto>> Handle(GetPatientAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _repository.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<MedicalHistoryAppointmentDto>>(appointments);
    }
}
