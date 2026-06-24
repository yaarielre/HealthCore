using MediatR;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, bool>
{
    private readonly IPatientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Patient with id {request.Id} not found");

        await _repository.DeleteAsync(patient.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}