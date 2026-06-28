using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Patients.Queries.GetPatientMedicalHistory;

public class GetPatientMedicalHistoryQueryHandler : IRequestHandler<GetPatientMedicalHistoryQuery, PatientMedicalHistoryDto>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public GetPatientMedicalHistoryQueryHandler(
        IPatientRepository patientRepository,
        IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<PatientMedicalHistoryDto> Handle(GetPatientMedicalHistoryQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetWithDetailsAsync(request.PatientId)
            ?? throw new KeyNotFoundException($"Patient with id {request.PatientId} not found");

        return _mapper.Map<PatientMedicalHistoryDto>(patient);
    }
}
