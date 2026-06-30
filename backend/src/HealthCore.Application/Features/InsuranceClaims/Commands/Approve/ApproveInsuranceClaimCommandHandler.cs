using AutoMapper;
using HealthCore.Application.Features.InsuranceClaims.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Commands.Approve;

public class ApproveInsuranceClaimCommandHandler : IRequestHandler<ApproveInsuranceClaimCommand, InsuranceClaimDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ApproveInsuranceClaimCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InsuranceClaimDto> Handle(ApproveInsuranceClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = await _unitOfWork.InsuranceClaims.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Reclamo con Id {request.Id} no encontrado.");

        if (claim.Status != Domain.Enums.ClaimStatus.Pending)
            throw new ApplicationException("Solo se pueden aprobar reclamos en estado Pendiente.");

        claim.Status = Domain.Enums.ClaimStatus.Approved;
        claim.ApprovedAmount = request.ApprovedAmount;
        claim.ApprovedAt = DateTime.UtcNow;

        await _unitOfWork.InsuranceClaims.UpdateAsync(claim);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InsuranceClaimDto>(claim);
    }
}
