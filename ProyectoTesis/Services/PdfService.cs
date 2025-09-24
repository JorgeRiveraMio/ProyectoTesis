using ProyectoTesis.Models.ViewModels;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using System;

namespace ProyectoTesis.Services
{
    public class PdfService
    {
        public byte[] GenerarPdf(ResultadoViewModel resultado)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);

                    page.Header().Text("Resultados del Test Vocacional")
                        .FontSize(20).Bold().AlignCenter();

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"📌 Perfil RIASEC: {resultado.PerfilRiasec}");
                        col.Item().Text($"📌 Perfil MBTI: {resultado.PerfilMbti}");
                        col.Item().Text($"🔗 Perfil Combinado: {resultado.PerfilCombinado}");
                        col.Item().Text($"📝 Descripción MBTI: {resultado.DescripcionMbti}");
                        col.Item().Text($"🎯 Recomendación: {resultado.DES_RECOMENDACION_TX}");

                        col.Item().Text("📚 Carreras sugeridas:").Bold();
                        foreach (var c in resultado.Carreras)
                        {
                            col.Item().Text($"- {c.Nombre} ({string.Join(", ", c.Universidades)})");
                        }
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Vocacional App - ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy")).FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }
    }
}
