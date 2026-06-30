using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalRecords.Queries.GetAllMedicalRecords;

public class GetAllMedicalRecordsQueryHandler : IRequestHandler<GetAllMedicalRecordsQuery, IEnumerable<MedicalRecordDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllMedicalRecordsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicalRecordDto>> Handle(GetAllMedicalRecordsQuery request, CancellationToken cancellationToken)
    {
        var records = await _unitOfWork.MedicalRecords.GetAllAsync();
        return _mapper.Map<IEnumerable<MedicalRecordDto>>(records);
    }
}
