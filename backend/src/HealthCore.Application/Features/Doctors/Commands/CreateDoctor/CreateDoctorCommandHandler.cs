using AutoMapper;
using HealthCore.Application.Features.Doctors.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, DoctorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDoctorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DoctorDto> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var specialty = await _unitOfWork.Specialties.GetByIdAsync(request.SpecialtyId)
            ?? throw new KeyNotFoundException($"Especialidad con Id {request.SpecialtyId} no encontrada.");

        if (await _unitOfWork.Doctors.ExistsByLicenseNumberAsync(request.LicenseNumber))
            throw new ApplicationException($"Ya existe un doctor con el exequátur '{request.LicenseNumber}'.");

        var doctor = new Doctor
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            LicenseNumber = request.LicenseNumber,
            SpecialtyId = request.SpecialtyId,
            IsActive = true
        };

        await _unitOfWork.Doctors.AddAsync(doctor);
        await _unitOfWork.SaveChangesAsync();

        doctor.Specialty = specialty;
        return _mapper.Map<DoctorDto>(doctor);
    }
}