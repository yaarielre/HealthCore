using HealthCore.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HealthCore.Infrastructure.Services;

public class PdfService : IPdfService
{
    public byte[] GeneratePrescriptionPdf(
        string patientName,
        string doctorName,
        string medication,
        string dosage,
        string frequency,
        string duration,
        string? instructions,
        DateTime fecha)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A5);

                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Text("HealthCore")
                        .FontSize(18).Bold().FontColor(Colors.Blue.Darken3);

                    col.Item().AlignCenter().Text("Sistema de Gestión Médica")
                        .FontSize(10).FontColor(Colors.Grey.Darken2);

                    col.Item().PaddingVertical(8).LineHorizontal(1).LineColor(Colors.Black);
                });

                page.Content().Column(col =>
                {
                    col.Item().PaddingBottom(10).Column(info =>
                    {
                        info.Item().Text($"Paciente: {patientName}").FontSize(11);
                        info.Item().Text($"Médico: {doctorName}").FontSize(11);
                        info.Item().Text($"Fecha: {fecha:dd/MM/yyyy}").FontSize(11);
                    });

                    col.Item().PaddingVertical(6).Border(1).BorderColor(Colors.Black).Padding(12).Column(pres =>
                    {
                        pres.Item().Text(medication).FontSize(14).Bold();
                        pres.Item().PaddingTop(6).Text($"Dosis: {dosage}").FontSize(11);
                        pres.Item().Text($"Frecuencia: {frequency}").FontSize(11);
                        pres.Item().Text($"Duración: {duration}").FontSize(11);

                        if (!string.IsNullOrEmpty(instructions))
                            pres.Item().PaddingTop(4).Text($"Indicaciones: {instructions}").FontSize(11).Italic();
                    });
                });

                page.Footer().Column(col =>
                {
                    col.Item().PaddingTop(20).LineHorizontal(1).LineColor(Colors.Black);
                    col.Item().PaddingTop(4).AlignCenter().Text(doctorName).FontSize(10);
                    col.Item().AlignCenter().Text($"Documento generado por HealthCore - {fecha:dd/MM/yyyy HH:mm}")
                        .FontSize(8).FontColor(Colors.Grey.Darken2);
                });
            });
        }).GeneratePdf();
    }
}
