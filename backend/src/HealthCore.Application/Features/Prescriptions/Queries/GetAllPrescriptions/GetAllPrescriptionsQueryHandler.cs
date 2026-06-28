using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Prescriptions.DTOs;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Prescriptions.Queries.GetAllPrescriptions;

public class GetAllPrescriptionsQueryHandler : IRequestHandler<GetAllPrescriptionsQuery, IEnumerable<PrescriptionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllPrescriptionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PrescriptionDto>> Handle(GetAllPrescriptionsQuery request, CancellationToken cancellationToken)
    {
        var prescriptions = await _unitOfWork.Prescriptions.GetAllAsync();
        return _mapper.Map<IEnumerable<PrescriptionDto>>(prescriptions);
    }
}
