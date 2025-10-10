#nullable disable
using ProyectoTesis.Models.ViewModels;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using System;
using System.Linq;

namespace ProyectoTesis.Services
{
    public class PdfService
    {
        public byte[] GenerarPdf(ResultadoViewModel resultado)
        {
            var colorPrimario = Colors.Blue.Medium;
            var colorTexto = Colors.Grey.Darken3;
            var colorLineas = Colors.Grey.Lighten1;

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.PageColor(Colors.White);

                    // === HEADER ===
                    page.Header().AlignCenter().Column(header =>
                    {
                        header.Spacing(5);
                        header.Item().Text("Reporte de Resultados Vocacionales")
                            .FontSize(24).Bold().FontColor(colorPrimario);
                        header.Item().Text("Evaluación Vocacional 2025")
                            .FontSize(11).Italic().FontColor(Colors.Grey.Darken1);
                    });

                    // === CONTENIDO ===
                    page.Content().PaddingVertical(30).Column(col =>
                    {
                        col.Spacing(25);

                        // === PERFIL GENERAL ===
                        col.Item().AlignLeft().Column(c =>
                        {
                            c.Spacing(4);
                            c.Item().Text("Perfil RIASEC").Bold().FontSize(14).FontColor(colorTexto);
                            c.Item().Text(resultado.PerfilRiasec)
                                .FontSize(20).Bold().FontColor(colorPrimario);

                            if (resultado.PuntajesOcean?.Any() == true)
                            {
                                c.Item().Text("Perfil OCEAN (Big Five)")
                                    .Bold().FontSize(14).FontColor(colorTexto);
                                c.Item().Text(resultado.PerfilOceanResumen ?? "")
                                    .FontSize(16).Bold().FontColor(colorPrimario);
                            }

                            c.Item().Text($"Perfil Combinado: {resultado.PerfilCombinado}")
                                .FontSize(12).Italic().FontColor(Colors.Grey.Darken2);
                        });

                        col.Item().LineHorizontal(1).LineColor(colorLineas);

                        // === RESULTADOS RIASEC ===
                        if (resultado.PuntajesRiasec?.Any() == true)
                        {
                            col.Item().Text("Resultados RIASEC")
                                .Bold().FontSize(16).FontColor(colorPrimario);

                            foreach (var cat in resultado.PuntajesRiasec)
                            {
                                var porcentaje = resultado.TotalRiasec > 0
                                    ? (cat.Value * 100 / resultado.TotalRiasec)
                                    : 0;

                                col.Item().Text($"{cat.Key}: {porcentaje}%").FontSize(12);
                            }

                            col.Item().PaddingVertical(10).LineHorizontal(0.5f).LineColor(colorLineas);
                        }

                        // === RESULTADOS OCEAN ===
                        if (resultado.PuntajesOcean?.Any() == true)
                        {
                            col.Item().Text("Resultados OCEAN (Big Five)")
                                .Bold().FontSize(16).FontColor(colorPrimario);

                            foreach (var factor in resultado.PuntajesOcean)
                            {
                                col.Item().Text($"{factor.Trait}: {factor.Value:F1}");
                            }

                            col.Item().PaddingVertical(10).LineHorizontal(0.5f).LineColor(colorLineas);
                        }

                        // === CARRERAS SUGERIDAS ===
                        if (resultado.Carreras?.Any() == true)
                        {
                            col.Item().Text("Carreras sugeridas")
                                .Bold().FontSize(16).FontColor(colorPrimario);

                            for (int i = 0; i < resultado.Carreras.Count; i++)
                            {
                                var carrera = resultado.Carreras[i];

                                col.Item().Element(e =>
                                {
                                    // ✅ cada carrera dentro de su propia columna
                                    e.ShowEntire().BorderBottom(1).BorderColor(colorLineas)
                                     .PaddingVertical(8)
                                     .Column(card =>
                                     {
                                         card.Spacing(4);

                                         card.Item().Text($"{i + 1}. {carrera.Nombre ?? "[Sin nombre]"}")
                                             .Bold().FontSize(14).FontColor(colorPrimario);

                                         if (!string.IsNullOrWhiteSpace(carrera.Descripcion))
                                             card.Item().Text($"Descripción: {carrera.Descripcion}")
                                                 .FontSize(11).FontColor(colorTexto);

                                         if (carrera.Score > 0)
                                             card.Item().Text($"Afinidad con tu perfil: {carrera.Score:F2}%")
                                                 .FontSize(11).FontColor(Colors.Grey.Darken2);

                                         if (carrera.Universidades?.Any() == true)
                                         {
                                             card.Item().Text("Universidades recomendadas:")
                                                 .Bold().FontSize(11).FontColor(Colors.Grey.Darken2);
                                             card.Item().Text(string.Join(", ", carrera.Universidades))
                                                 .FontSize(10).FontColor(Colors.Grey.Darken1);
                                         }
                                     });
                                });
                            }
                        }
                        else
                        {
                            col.Item().Text("⚠️ No hay carreras cargadas en el ViewModel.");
                        }
                    });

                    // === FOOTER ===
                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Vocacional App © 2025").FontSize(9).FontColor(Colors.Grey.Darken1);
                        txt.Span($"  •  Generado el {DateTime.Now:dd/MM/yyyy}")
                            .FontSize(9).FontColor(Colors.Grey.Darken1);
                    });
                });
            });

            return pdf.GeneratePdf();
        }
    }
}
