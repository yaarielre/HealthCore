using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationsByPatient;

public class GetMedicalConsultationsByPatientQueryHandler : IRequestHandler<GetMedicalConsultationsByPatientQuery, IEnumerable<MedicalConsultationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMedicalConsultationsByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicalConsultationDto>> Handle(GetMedicalConsultationsByPatientQuery request, CancellationToken cancellationToken)
    {
        var consultations = await _unitOfWork.MedicalConsultations.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<MedicalConsultationDto>>(consultations);
    }
}
