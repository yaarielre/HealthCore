using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalHistoryItems.Queries.GetAll;

public class GetAllMedicalHistoryItemsQueryHandler : IRequestHandler<GetAllMedicalHistoryItemsQuery, IEnumerable<MedicalHistoryItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllMedicalHistoryItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicalHistoryItemDto>> Handle(GetAllMedicalHistoryItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.MedicalHistoryItems.GetAllAsync();
        return _mapper.Map<IEnumerable<MedicalHistoryItemDto>>(items);
    }
}
