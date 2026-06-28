namespace HealthCore.Application.Interfaces;

public interface IPdfService
{
    byte[] GeneratePrescriptionPdf(
        string patientName,
        string doctorName,
        string medication,
        string dosage,
        string frequency,
        string duration,
        string? instructions,
        DateTime fecha);
}
