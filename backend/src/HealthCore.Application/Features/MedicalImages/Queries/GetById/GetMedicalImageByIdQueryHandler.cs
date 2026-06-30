using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalImages.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalImages.Queries.GetById;

public class GetMedicalImageByIdQueryHandler : IRequestHandler<GetMedicalImageByIdQuery, MedicalImageDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMedicalImageByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalImageDto?> Handle(GetMedicalImageByIdQuery request, CancellationToken cancellationToken)
    {
        var image = await _unitOfWork.MedicalImages.GetByIdAsync(request.Id);
        return image is null ? null : _mapper.Map<MedicalImageDto>(image);
    }
}
