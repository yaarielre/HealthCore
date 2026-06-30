using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalRecords.Queries.GetMedicalRecordById;

public class GetMedicalRecordByIdQueryHandler : IRequestHandler<GetMedicalRecordByIdQuery, MedicalRecordDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMedicalRecordByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalRecordDto?> Handle(GetMedicalRecordByIdQuery request, CancellationToken cancellationToken)
    {
        var record = await _unitOfWork.MedicalRecords.GetByIdAsync(request.Id);
        return record is null ? null : _mapper.Map<MedicalRecordDto>(record);
    }
}
