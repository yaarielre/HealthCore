using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalConsultations.Queries.GetAllMedicalConsultations;

public class GetAllMedicalConsultationsQueryHandler : IRequestHandler<GetAllMedicalConsultationsQuery, IEnumerable<MedicalConsultationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllMedicalConsultationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicalConsultationDto>> Handle(GetAllMedicalConsultationsQuery request, CancellationToken cancellationToken)
    {
        var consultations = await _unitOfWork.MedicalConsultations.GetAllAsync();
        return _mapper.Map<IEnumerable<MedicalConsultationDto>>(consultations);
    }
}
