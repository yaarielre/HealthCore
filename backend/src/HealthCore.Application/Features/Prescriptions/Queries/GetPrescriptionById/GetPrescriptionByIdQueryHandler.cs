using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Prescriptions.Queries.GetPrescriptionById;

public class GetPrescriptionByIdQueryHandler : IRequestHandler<GetPrescriptionByIdQuery, PrescriptionDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPrescriptionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PrescriptionDto?> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(request.Id);
        return prescription is null ? null : _mapper.Map<PrescriptionDto>(prescription);
    }
}
