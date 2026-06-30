using AutoMapper;
using HealthCore.Application.Features.InsuranceClaims.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Queries.GetAll;

public class GetAllInsuranceClaimsQueryHandler : IRequestHandler<GetAllInsuranceClaimsQuery, IEnumerable<InsuranceClaimDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllInsuranceClaimsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InsuranceClaimDto>> Handle(GetAllInsuranceClaimsQuery request, CancellationToken cancellationToken)
    {
        var claims = await _unitOfWork.InsuranceClaims.GetAllAsync();
        return _mapper.Map<IEnumerable<InsuranceClaimDto>>(claims);
    }
}
