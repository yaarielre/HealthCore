using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionsByPatient;

public class GetPrescriptionsByPatientQueryHandler : IRequestHandler<GetPrescriptionsByPatientQuery, IEnumerable<PrescriptionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPrescriptionsByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PrescriptionDto>> Handle(GetPrescriptionsByPatientQuery request, CancellationToken cancellationToken)
    {
        var prescriptions = await _unitOfWork.Prescriptions.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<PrescriptionDto>>(prescriptions);
    }
}
