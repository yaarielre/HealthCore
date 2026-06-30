using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalHistoryItems.Queries.GetByPatient;

public class GetMedicalHistoryItemsByPatientQueryHandler : IRequestHandler<GetMedicalHistoryItemsByPatientQuery, IEnumerable<MedicalHistoryItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMedicalHistoryItemsByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicalHistoryItemDto>> Handle(GetMedicalHistoryItemsByPatientQuery request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.MedicalHistoryItems.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<MedicalHistoryItemDto>>(items);
    }
}
