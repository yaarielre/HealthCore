using MediatR;

namespace HealthCore.Application.Features.MedicalRecords.Commands.DeleteMedicalRecord;

public record DeleteMedicalRecordCommand(Guid Id) : IRequest<bool>;
