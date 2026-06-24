using AutoMapper;
using HealthCore.Application.Features.Doctors.DTOs;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, DoctorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDoctorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DoctorDto> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Doctor con Id {request.Id} no encontrado.");

        if (await _unitOfWork.Specialties.GetByIdAsync(request.SpecialtyId) is null)
            throw new KeyNotFoundException($"Especialidad con Id {request.SpecialtyId} no encontrada.");

        if (await _unitOfWork.Doctors.ExistsByLicenseNumberAsync(request.LicenseNumber, request.Id))
            throw new ApplicationException($"Ya existe otro doctor con el exequátur '{request.LicenseNumber}'.");

        doctor.FirstName = request.FirstName;
        doctor.LastName = request.LastName;
        doctor.LicenseNumber = request.LicenseNumber;
        doctor.SpecialtyId = request.SpecialtyId;

        await _unitOfWork.Doctors.UpdateAsync(doctor);
        await _unitOfWork.SaveChangesAsync();

        var updated = await _unitOfWork.Doctors.GetByIdAsync(doctor.Id);
        return _mapper.Map<DoctorDto>(updated);
    }
}