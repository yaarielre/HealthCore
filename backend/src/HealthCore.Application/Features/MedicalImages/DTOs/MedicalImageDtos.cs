namespace HealthCore.Application.Features.MedicalImages.DTOs;

public record MedicalImageDto(
    Guid Id,
    Guid PatientId,
    string PatientName,
    Guid? MedicalConsultationId,
    Guid? OrderItemId,
    string ImageType,
    string? BodyPart,
    string FileName,
    string FilePath,
    string ContentType,
    long FileSizeBytes,
    string? Description,
    string? Interpretation,
    Guid? InterpretedById,
    string? InterpretedByName,
    DateTime? TakenAt,
    DateTime CreatedAt
);

public record CreateMedicalImageDto(
    Guid PatientId,
    Guid? MedicalConsultationId,
    Guid? OrderItemId,
    string ImageType,
    string? BodyPart,
    string FileName,
    string FilePath,
    string ContentType,
    long FileSizeBytes,
    string? Description,
    string? Interpretation,
    Guid? InterpretedById,
    DateTime? TakenAt
);

public record UpdateMedicalImageDto(
    string ImageType,
    string? BodyPart,
    string FileName,
    string FilePath,
    string ContentType,
    long FileSizeBytes,
    string? Description,
    string? Interpretation,
    Guid? InterpretedById,
    DateTime? TakenAt
);
