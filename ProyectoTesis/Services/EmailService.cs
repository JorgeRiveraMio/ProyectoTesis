using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProyectoTesis.Services
{
    public class EmailService
    {
        private readonly string _apiKey;
        private readonly string _fromEmail;

        public EmailService(IConfiguration configuration)
        {
            // Lee desde User Secrets en local o variables de entorno en producción
            _apiKey = configuration["BREVO_API_KEY"];
            _fromEmail = configuration["FROM_EMAIL"] ?? "marcofabianj@hotmail.com";
        }

        /// <summary>
        /// Envía un correo con cuerpo HTML y un PDF adjunto utilizando la API HTTP de Brevo.
        /// </summary>
        public async Task EnviarCorreoConPdfAsync(string destinatario, string asunto, string cuerpoHtml, byte[] pdfBytes)
        {
            if (string.IsNullOrEmpty(_apiKey))
                throw new InvalidOperationException("La variable BREVO_API_KEY no está configurada.");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("api-key", _apiKey);

            var payload = new
            {
                sender = new { email = _fromEmail, name = "Vocacional App" },
                to = new[] { new { email = destinatario } },
                subject = asunto,
                htmlContent = cuerpoHtml,
                attachment = (pdfBytes != null && pdfBytes.Length > 0)
                    ? new[] {
                        new {
                            content = Convert.ToBase64String(pdfBytes),
                            name = "ResultadosVocacionales.pdf"
                        }
                    }
                    : null
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.brevo.com/v3/smtp/email", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error al enviar correo: {response.StatusCode}\n{error}");
                Console.ResetColor();
                throw new Exception($"Error al enviar correo: {response.StatusCode} - {error}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Correo enviado correctamente con la API HTTP de Brevo.");
            Console.ResetColor();
        }
    }
}
