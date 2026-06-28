using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Patients.Queries.SearchPatients;

public class SearchPatientsQueryHandler : IRequestHandler<SearchPatientsQuery, IEnumerable<PatientSearchDto>>
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;

    public SearchPatientsQueryHandler(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientSearchDto>> Handle(SearchPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await _repository.SearchAsync(request.Term);
        var paged = patients
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);
        return _mapper.Map<IEnumerable<PatientSearchDto>>(paged);
    }
}
