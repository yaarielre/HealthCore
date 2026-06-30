using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Immunizations.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Immunizations.Commands.Create;

public class CreateImmunizationCommandHandler : IRequestHandler<CreateImmunizationCommand, ImmunizationDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateImmunizationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ImmunizationDto> Handle(CreateImmunizationCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(request.Dto.PatientId)
            ?? throw new KeyNotFoundException($"Paciente con Id '{request.Dto.PatientId}' no encontrado.");

        var immunization = new Immunization
        {
            Id = Guid.NewGuid(),
            PatientId = request.Dto.PatientId,
            VaccineName = request.Dto.VaccineName,
            DoseNumber = request.Dto.DoseNumber,
            ApplicationDate = request.Dto.ApplicationDate,
            NextDoseDate = request.Dto.NextDoseDate,
            BatchNumber = request.Dto.BatchNumber,
            AdministeredBy = request.Dto.AdministeredBy,
            Notes = request.Dto.Notes
        };

        await _unitOfWork.Immunizations.AddAsync(immunization);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ImmunizationDto>(immunization) with
        {
            PatientName = $"{patient.FirstName} {patient.LastName}"
        };
    }
}
