using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Patients.Queries.GetPatientByIdNumber;

public class GetPatientByIdNumberQueryHandler : IRequestHandler<GetPatientByIdNumberQuery, PatientDto>
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;

    public GetPatientByIdNumberQueryHandler(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PatientDto> Handle(GetPatientByIdNumberQuery request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdNumberAsync(request.IdNumber)
            ?? throw new KeyNotFoundException($"Patient with id number {request.IdNumber} not found");

        return _mapper.Map<PatientDto>(patient);
    }
}
