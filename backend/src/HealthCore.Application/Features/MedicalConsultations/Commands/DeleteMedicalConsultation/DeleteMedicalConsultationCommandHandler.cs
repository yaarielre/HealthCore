using MediatR;
using HealthCore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthCore.Application.Features.MedicalConsultations.Commands.DeleteMedicalConsultation;

public class DeleteMedicalConsultationCommandHandler : IRequestHandler<DeleteMedicalConsultationCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteMedicalConsultationCommandHandler> _logger;

    public DeleteMedicalConsultationCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteMedicalConsultationCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMedicalConsultationCommand request, CancellationToken cancellationToken)
    {
        var consultation = await _unitOfWork.MedicalConsultations.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Consulta con Id '{request.Id}' no encontrada.");

        _logger.LogInformation("Consulta eliminada: {Id}", consultation.Id);

        await _unitOfWork.MedicalConsultations.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
