using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalConsultations.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalConsultations.Queries.GetMedicalConsultationById;

public class GetMedicalConsultationByIdQueryHandler : IRequestHandler<GetMedicalConsultationByIdQuery, MedicalConsultationDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMedicalConsultationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalConsultationDto?> Handle(GetMedicalConsultationByIdQuery request, CancellationToken cancellationToken)
    {
        var consultation = await _unitOfWork.MedicalConsultations.GetWithDetailsAsync(request.Id);
        return consultation is null ? null : _mapper.Map<MedicalConsultationDto>(consultation);
    }
}
