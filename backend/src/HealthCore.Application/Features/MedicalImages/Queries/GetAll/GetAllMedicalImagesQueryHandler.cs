using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalImages.Queries.GetAll;

public class GetAllMedicalImagesQueryHandler : IRequestHandler<GetAllMedicalImagesQuery, IEnumerable<MedicalImageDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllMedicalImagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicalImageDto>> Handle(GetAllMedicalImagesQuery request, CancellationToken cancellationToken)
    {
        var images = await _unitOfWork.MedicalImages.GetAllAsync();
        return _mapper.Map<IEnumerable<MedicalImageDto>>(images);
    }
}
