using AutoMapper;
using HealthCore.Application.Features.Doctors.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Doctors.Queries.GetDoctorsBySpecialty;

public class GetDoctorsBySpecialtyQueryHandler : IRequestHandler<GetDoctorsBySpecialtyQuery, IEnumerable<DoctorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDoctorsBySpecialtyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetDoctorsBySpecialtyQuery request, CancellationToken cancellationToken)
    {
        var doctors = await _unitOfWork.Doctors.GetBySpecialtyAsync(request.SpecialtyId);
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }
}