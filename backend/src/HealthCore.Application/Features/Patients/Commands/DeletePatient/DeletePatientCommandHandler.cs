using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, bool>
{
    private readonly IPatientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeletePatientCommandHandler> _logger;

    public DeletePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfWork, ILogger<DeletePatientCommandHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Patient with id {request.Id} not found");

        _logger.LogInformation("Paciente eliminado (soft): {Id} ({FirstName} {LastName})", patient.Id, patient.FirstName, patient.LastName);
        patient.IsDeleted = true;
        patient.IsActive = false;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}