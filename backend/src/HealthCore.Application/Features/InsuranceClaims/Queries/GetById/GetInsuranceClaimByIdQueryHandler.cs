using AutoMapper;
using HealthCore.Application.Features.InsuranceClaims.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Queries.GetById;

public class GetInsuranceClaimByIdQueryHandler : IRequestHandler<GetInsuranceClaimByIdQuery, InsuranceClaimDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetInsuranceClaimByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InsuranceClaimDto> Handle(GetInsuranceClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var claim = await _unitOfWork.InsuranceClaims.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Reclamo con Id {request.Id} no encontrado.");

        return _mapper.Map<InsuranceClaimDto>(claim);
    }
}
