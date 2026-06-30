using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalRecords.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalRecords.Queries.GetMedicalRecordByPatient;

public class GetMedicalRecordByPatientQueryHandler : IRequestHandler<GetMedicalRecordByPatientQuery, MedicalRecordDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMedicalRecordByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalRecordDto?> Handle(GetMedicalRecordByPatientQuery request, CancellationToken cancellationToken)
    {
        var record = await _unitOfWork.MedicalRecords.GetByPatientIdAsync(request.PatientId);
        return record is null ? null : _mapper.Map<MedicalRecordDto>(record);
    }
}
