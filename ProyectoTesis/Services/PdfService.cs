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

                    page.Header().AlignCenter().Column(header =>
                    {
                        header.Spacing(5);
                        header.Item().Text("Reporte de Resultados Vocacionales")
                            .FontSize(24).Bold().FontColor(colorPrimario);
                        header.Item().Text("Evaluación Vocacional 2025")
                            .FontSize(11).Italic().FontColor(Colors.Grey.Darken1);
                    });

                    page.Content().PaddingVertical(30).Column(col =>
                    {
                        col.Spacing(25);

                        col.Item().AlignLeft().Column(c =>
                        {
                            c.Spacing(4);
                            c.Item().Text("Perfil RIASEC")
                                .Bold().FontSize(14).FontColor(colorTexto);
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

                        if (resultado.PuntajesRiasec?.Any() == true)
                        {
                            col.Item().Container().PaddingBottom(10)
                                .Text("Resultados RIASEC")
                                .Bold().FontSize(16).FontColor(colorPrimario);

                            foreach (var cat in resultado.PuntajesRiasec)
                            {
                                var porcentaje = resultado.TotalRiasec > 0
                                    ? (cat.Value * 100 / resultado.TotalRiasec)
                                    : 0;

                                col.Item().Row(row =>
                                {
                                    row.AutoItem().Text($"{cat.Key}: {porcentaje}%")
                                        .FontSize(12).FontColor(colorTexto);

                                    row.RelativeItem().Column(stack =>
                                    {
                                        stack.Item().Height(8).Background(Colors.Grey.Lighten2);
                                        stack.Item().Width(porcentaje).Height(8)
                                            .Background(colorPrimario);
                                    });
                                });
                            }

                            col.Item().PaddingVertical(10)
                                .LineHorizontal(0.5f).LineColor(colorLineas);
                        }

                        if (resultado.PuntajesOcean?.Any() == true)
                        {
                            col.Item().Container().PaddingBottom(10)
                                .Text("Resultados OCEAN (Big Five)")
                                .Bold().FontSize(16).FontColor(colorPrimario);

                            foreach (var factor in resultado.PuntajesOcean)
                            {
                                var trait = factor.Trait;
                                var valor = factor.Value;

                                col.Item().Row(row =>
                                {
                                    row.AutoItem().Text($"{trait}: {valor:F1}")
                                        .FontSize(12).FontColor(colorTexto);

                                    row.RelativeItem().Column(stack =>
                                    {
                                        stack.Item().Height(8).Background(Colors.Grey.Lighten2);
                                        stack.Item().Width((int)(valor * 20))
                                            .Height(8)
                                            .Background(Colors.Green.Medium);
                                    });
                                });
                            }

                            col.Item().PaddingVertical(10)
                                .LineHorizontal(0.5f).LineColor(colorLineas);
                        }

                        if (resultado.Carreras?.Any() == true)
                        {
                            col.Item().Container().PaddingBottom(10)
                                .Text("Carreras sugeridas")
                                .Bold().FontSize(16).FontColor(colorPrimario);

                            foreach (var carrera in resultado.Carreras)
                            {
                                var colorBarra = carrera.Score >= 80 ? Colors.Green.Medium
                                               : carrera.Score >= 60 ? Colors.Yellow.Medium
                                               : Colors.Red.Medium;

                                col.Item().Column(card =>
                                {
                                    card.Spacing(4);
                                    card.Item().Text(carrera.Nombre)
                                        .Bold().FontSize(14).FontColor(colorPrimario);

                                    if (!string.IsNullOrWhiteSpace(carrera.Descripcion))
                                        card.Item().Text(carrera.Descripcion)
                                            .FontSize(12).LineHeight(1.4f).FontColor(colorTexto);

                                    if (carrera.Score > 0)
                                    {
                                        card.Item().Text($"Afinidad con tu perfil: {carrera.Score:F2}%")
                                            .FontSize(11).FontColor(Colors.Grey.Darken2);

                                        var ancho = Math.Min(carrera.Score, 100);
                                        string svg = $@"
<svg width='100%' height='8' xmlns='http://www.w3.org/2000/svg'>
  <rect width='100%' height='8' fill='{Colors.Grey.Lighten2}' />
  <rect width='{ancho}%' height='8' fill='{colorBarra}' />
</svg>";
                                        card.Item().Svg(svg);
                                    }

                                    if (carrera.Universidades?.Any() == true)
                                    {
                                        card.Item().Text("Universidades recomendadas:")
                                            .Bold().FontSize(11).FontColor(Colors.Grey.Darken2);
                                        card.Item().Text(string.Join(", ", carrera.Universidades))
                                            .FontSize(11).FontColor(Colors.Grey.Darken1);
                                    }
                                });

                                col.Item().PaddingVertical(8)
                                    .LineHorizontal(0.5f).LineColor(colorLineas);
                            }
                        }
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Vocacional App © 2025")
                            .FontSize(9).FontColor(Colors.Grey.Darken1);
                        txt.Span($"  •  Generado el {DateTime.Now:dd/MM/yyyy}")
                            .FontSize(9).FontColor(Colors.Grey.Darken1);
                    });
                });
            });

            return pdf.GeneratePdf();
        }
    }
}
