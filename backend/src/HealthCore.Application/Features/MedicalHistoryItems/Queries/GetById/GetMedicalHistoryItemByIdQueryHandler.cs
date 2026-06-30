using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalHistoryItems.Queries.GetById;

public class GetMedicalHistoryItemByIdQueryHandler : IRequestHandler<GetMedicalHistoryItemByIdQuery, MedicalHistoryItemDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMedicalHistoryItemByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalHistoryItemDto?> Handle(GetMedicalHistoryItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.MedicalHistoryItems.GetByIdAsync(request.Id);
        return item is null ? null : _mapper.Map<MedicalHistoryItemDto>(item);
    }
}
