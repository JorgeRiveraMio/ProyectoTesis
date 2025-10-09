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
            var colorSecundario = Colors.Indigo.Lighten3;
            var colorTexto = Colors.Grey.Darken3;

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);

                    // ==========================
                    // ENCABEZADO
                    // ==========================
                    page.Header()
                        .Text("Reporte de Resultados Vocacionales")
                        .FontSize(24)
                        .Bold()
                        .FontColor(colorPrimario)
                        .AlignCenter();

                    // ==========================
                    // CONTENIDO PRINCIPAL
                    // ==========================
                    page.Content().Column(col =>
                    {
                        col.Spacing(20);

                        // ----- Bloque de perfiles -----
                        col.Item().Container()
                            .Background(colorSecundario)
                            .Padding(12)
                            .Border(1)
                            .BorderColor(colorPrimario)
                            .Column(c =>
                        {
                            c.Item().Text("Perfil RIASEC")
                                .Bold().FontSize(14).FontColor(colorTexto);

                            c.Item().Text(resultado.PerfilRiasec)
                                .FontSize(16).Bold().FontColor(colorPrimario);

                            if (resultado.PuntajesOcean?.Any() == true)
                            {
                                c.Item().Text("Perfil OCEAN (Big Five)")
                                    .Bold().FontSize(14).FontColor(colorTexto);

                                c.Item().Text(resultado.PerfilOceanResumen ?? "")
                                    .FontSize(16).Bold().FontColor(colorPrimario);
                            }

                            c.Item().Text($"Perfil Combinado: {resultado.PerfilCombinado}")
                                .FontSize(13).Italic().FontColor(colorTexto);
                        });

                        // ----- Resultados RIASEC -----
                        if (resultado.PuntajesRiasec?.Any() == true)
                        {
                            col.Item().Text("Resultados RIASEC")
                                .FontSize(16).Bold().FontColor(colorPrimario);

                            foreach (var cat in resultado.PuntajesRiasec)
                            {
                                var porcentaje = resultado.TotalRiasec > 0
                                    ? (cat.Value * 100 / resultado.TotalRiasec)
                                    : 0;

                                col.Item().Row(row =>
                                {
                                    row.AutoItem()
                                        .Text($"{cat.Key}: {porcentaje}%")
                                        .FontSize(12);

                                    row.RelativeItem().Column(stack =>
                                    {
                                        stack.Item().Height(8).Background(Colors.Grey.Lighten2);
                                        stack.Item().Width(porcentaje).Height(8).Background(colorPrimario);
                                    });
                                });
                            }
                        }

                        // ----- Resultados OCEAN -----
                        if (resultado.PuntajesOcean?.Any() == true)
                        {
                            col.Item().Text("Resultados OCEAN (Big Five)")
                                .FontSize(16).Bold().FontColor(colorPrimario);

                            foreach (var factor in resultado.PuntajesOcean)
                            {
                                var trait = factor.Trait;
                                var valor = factor.Value;

                                col.Item().Row(row =>
                                {
                                    row.AutoItem()
                                        .Text($"{trait}: {valor:F1}")
                                        .FontSize(12);

                                    row.RelativeItem().Column(stack =>
                                    {
                                        stack.Item().Height(8).Background(Colors.Grey.Lighten2);
                                        stack.Item().Width((int)(valor * 20))
                                            .Height(8)
                                            .Background(Colors.Green.Medium);
                                    });
                                });
                            }
                        }

                        // ----- Carreras sugeridas -----
                        if (resultado.Carreras?.Any() == true)
                        {
                            col.Item().Text("Carreras sugeridas")
                                .FontSize(16).Bold().FontColor(colorPrimario);

                            foreach (var carrera in resultado.Carreras)
                            {
                                col.Item().Container()
                                    .Border(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(10)
                                    .Column(card =>
                                {
                                    card.Spacing(2);
                                    card.Item().Text(carrera.Nombre)
                                        .Bold().FontSize(13).FontColor(colorPrimario);

                                    if (!string.IsNullOrWhiteSpace(carrera.Descripcion))
                                        card.Item().Text(carrera.Descripcion)
                                            .FontSize(11).FontColor(colorTexto);

                                    if (carrera.Universidades?.Any() == true)
                                    {
                                        card.Item().Text($"Universidades de referencia: {string.Join(", ", carrera.Universidades)}")
                                            .FontSize(10).Italic().FontColor(Colors.Grey.Darken1);
                                    }
                                });
                            }
                        }
                    });

                    // ==========================
                    // PIE DE PÁGINA
                    // ==========================
                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Vocacional App ").FontSize(9).FontColor(Colors.Grey.Darken1);
                        txt.Span($"• Generado el {DateTime.Now:dd/MM/yyyy}")
                           .FontSize(9).FontColor(Colors.Grey.Darken1);
                    });
                });
            });

            return pdf.GeneratePdf();
        }
    }
}
