using AutoMapper;
using HealthCore.Application.Features.InsuranceClaims.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.InsuranceClaims.Commands.Create;

public class CreateInsuranceClaimCommandHandler : IRequestHandler<CreateInsuranceClaimCommand, InsuranceClaimDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateInsuranceClaimCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InsuranceClaimDto> Handle(CreateInsuranceClaimCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Invoices.GetByIdAsync(request.InvoiceId)
            ?? throw new KeyNotFoundException($"Factura con Id {request.InvoiceId} no encontrada.");

        var patient = await _unitOfWork.Patients.GetByIdAsync(request.PatientId)
            ?? throw new KeyNotFoundException($"Paciente con Id {request.PatientId} no encontrado.");

        var claim = new InsuranceClaim
        {
            InvoiceId = request.InvoiceId,
            PatientId = request.PatientId,
            InsuranceCompany = request.InsuranceCompany,
            PolicyNumber = request.PolicyNumber,
            ClaimAmount = request.ClaimAmount,
            Status = Domain.Enums.ClaimStatus.Pending,
            FiledAt = DateTime.UtcNow,
            Notes = request.Notes
        };

        await _unitOfWork.InsuranceClaims.AddAsync(claim);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InsuranceClaimDto>(claim);
    }
}
