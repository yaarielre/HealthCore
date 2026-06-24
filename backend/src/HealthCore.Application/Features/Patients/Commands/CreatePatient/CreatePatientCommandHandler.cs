using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Interfaces;
using HealthCore.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDto>
{
    private readonly IPatientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePatientCommandHandler> _logger;

    public CreatePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreatePatientCommandHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = _mapper.Map<Patient>(request.Dto);
        patient.Id = Guid.NewGuid();

        await _repository.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Paciente creado: {FirstName} {LastName} ({Cedula})", patient.FirstName, patient.LastName, patient.Cedula);
        return _mapper.Map<PatientDto>(patient);
    }
}