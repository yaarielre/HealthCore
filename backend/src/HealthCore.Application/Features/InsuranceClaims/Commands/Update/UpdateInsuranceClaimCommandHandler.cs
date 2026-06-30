using AutoMapper;
using HealthCore.Application.Features.InsuranceClaims.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Commands.Update;

public class UpdateInsuranceClaimCommandHandler : IRequestHandler<UpdateInsuranceClaimCommand, InsuranceClaimDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateInsuranceClaimCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InsuranceClaimDto> Handle(UpdateInsuranceClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = await _unitOfWork.InsuranceClaims.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Reclamo con Id {request.Id} no encontrado.");

        if (claim.Status != Domain.Enums.ClaimStatus.Pending)
            throw new ApplicationException("Solo se pueden modificar reclamos en estado Pendiente.");

        claim.InsuranceCompany = request.InsuranceCompany;
        claim.PolicyNumber = request.PolicyNumber;
        claim.ClaimAmount = request.ClaimAmount;
        claim.Notes = request.Notes;

        await _unitOfWork.InsuranceClaims.UpdateAsync(claim);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InsuranceClaimDto>(claim);
    }
}
