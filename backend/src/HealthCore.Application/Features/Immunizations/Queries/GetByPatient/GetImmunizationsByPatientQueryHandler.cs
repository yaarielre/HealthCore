using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Immunizations.Queries.GetByPatient;

public class GetImmunizationsByPatientQueryHandler : IRequestHandler<GetImmunizationsByPatientQuery, IEnumerable<ImmunizationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetImmunizationsByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ImmunizationDto>> Handle(GetImmunizationsByPatientQuery request, CancellationToken cancellationToken)
    {
        var immunizations = await _unitOfWork.Immunizations.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<ImmunizationDto>>(immunizations);
    }
}
