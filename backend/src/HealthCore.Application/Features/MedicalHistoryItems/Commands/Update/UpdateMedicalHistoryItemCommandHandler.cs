using AutoMapper;
using MediatR;
using HealthCore.Application.Features.MedicalHistoryItems.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.MedicalHistoryItems.Commands.Update;

public class UpdateMedicalHistoryItemCommandHandler : IRequestHandler<UpdateMedicalHistoryItemCommand, MedicalHistoryItemDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMedicalHistoryItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicalHistoryItemDto> Handle(UpdateMedicalHistoryItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.MedicalHistoryItems.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Item de historial con Id '{request.Id}' no encontrado.");

        _mapper.Map(request.Dto, item);
        item.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.MedicalHistoryItems.UpdateAsync(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MedicalHistoryItemDto>(item);
    }
}
