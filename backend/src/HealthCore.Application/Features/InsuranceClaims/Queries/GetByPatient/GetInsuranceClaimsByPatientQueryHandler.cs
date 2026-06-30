using AutoMapper;
using HealthCore.Application.Features.InsuranceClaims.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Queries.GetByPatient;

public class GetInsuranceClaimsByPatientQueryHandler : IRequestHandler<GetInsuranceClaimsByPatientQuery, IEnumerable<InsuranceClaimDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetInsuranceClaimsByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InsuranceClaimDto>> Handle(GetInsuranceClaimsByPatientQuery request, CancellationToken cancellationToken)
    {
        var claims = await _unitOfWork.InsuranceClaims.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<InsuranceClaimDto>>(claims);
    }
}
