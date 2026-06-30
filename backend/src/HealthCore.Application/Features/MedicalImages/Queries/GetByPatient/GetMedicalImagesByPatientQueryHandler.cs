using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalImages.Queries.GetByPatient;

public class GetMedicalImagesByPatientQueryHandler : IRequestHandler<GetMedicalImagesByPatientQuery, IEnumerable<MedicalImageDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMedicalImagesByPatientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicalImageDto>> Handle(GetMedicalImagesByPatientQuery request, CancellationToken cancellationToken)
    {
        var images = await _unitOfWork.MedicalImages.GetByPatientIdAsync(request.PatientId);
        return _mapper.Map<IEnumerable<MedicalImageDto>>(images);
    }
}
