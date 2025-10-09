using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace ProyectoTesis.Services
{
    public class EmailService
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("MAILJET_API_KEY");
        private readonly string _secretKey = Environment.GetEnvironmentVariable("MAILJET_SECRET_KEY");


        public async Task EnviarCorreoConPdfAsync(string destinatario, string asunto, string cuerpoHtml, byte[] pdfBytes)
        {
            var client = new MailjetClient(_apiKey, _secretKey);
            var request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "marcofabianjz@gmail.com")
            .Property(Send.FromName, "Vocacional App")
            .Property(Send.Subject, asunto)
            .Property(Send.HtmlPart, cuerpoHtml)
            .Property(Send.Recipients, new JArray {
                new JObject { { "Email", destinatario } }
            });

            if (pdfBytes != null && pdfBytes.Length > 0)
            {
                request.Property(Send.Attachments, new JArray {
                    new JObject {
                        { "ContentType", "application/pdf" },
                        { "Filename", "ResultadosVocacionales.pdf" },
                        { "Base64Content", Convert.ToBase64String(pdfBytes) }
                    }
                });
            }

            var response = await client.PostAsync(request);
            Console.WriteLine($"Mailjet response: {response.StatusCode}");
        }
    }
}
