using AutoMapper;
using HealthCore.Application.Features.InsuranceClaims.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Commands.Deny;

public class DenyInsuranceClaimCommandHandler : IRequestHandler<DenyInsuranceClaimCommand, InsuranceClaimDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DenyInsuranceClaimCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InsuranceClaimDto> Handle(DenyInsuranceClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = await _unitOfWork.InsuranceClaims.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Reclamo con Id {request.Id} no encontrado.");

        if (claim.Status != Domain.Enums.ClaimStatus.Pending)
            throw new ApplicationException("Solo se pueden denegar reclamos en estado Pendiente.");

        claim.Status = Domain.Enums.ClaimStatus.Denied;
        if (request.Notes != null)
            claim.Notes = request.Notes;

        await _unitOfWork.InsuranceClaims.UpdateAsync(claim);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InsuranceClaimDto>(claim);
    }
}
