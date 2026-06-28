using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Patients.Commands.RestorePatient;

public class RestorePatientCommandHandler : IRequestHandler<RestorePatientCommand, bool>
{
    private readonly IPatientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RestorePatientCommandHandler> _logger;

    public RestorePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfWork, ILogger<RestorePatientCommandHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(RestorePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Patient with id {request.Id} not found");

        patient.IsDeleted = false;
        patient.IsActive = true;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Paciente restaurado: {Id} ({FirstName} {LastName})", patient.Id, patient.FirstName, patient.LastName);
        return true;
    }
}
