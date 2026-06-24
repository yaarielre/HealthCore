using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Patients.DTOs;
using HealthCore.Domain.Interfaces;
using HealthCore.Domain.Entities;

namespace HealthCore.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDto>
{
    private readonly IPatientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = _mapper.Map<Patient>(request.Dto);
        patient.Id = Guid.NewGuid();

        await _repository.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PatientDto>(patient);
    }
}