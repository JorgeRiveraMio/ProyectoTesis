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
            .Property(Send.Messages, new JArray
            {
                new JObject
                {
                    ["From"] = new JObject
                    {
                        ["Email"] = "marcofabianjz@gmail.com",
                        ["Name"] = "Vocacional App"
                    },
                    ["To"] = new JArray
                    {
                        new JObject
                        {
                            ["Email"] = destinatario,
                            ["Name"] = "Usuario"
                        }
                    },
                    ["Subject"] = asunto,
                    ["HTMLPart"] = cuerpoHtml,
                    ["Attachments"] = pdfBytes != null && pdfBytes.Length > 0
                        ? new JArray
                        {
                            new JObject
                            {
                                ["ContentType"] = "application/pdf",
                                ["Filename"] = "ResultadosVocacionales.pdf",
                                ["Base64Content"] = Convert.ToBase64String(pdfBytes)
                            }
                        }
                        : null
                }
            });

            var response = await client.PostAsync(request);
            Console.WriteLine($"Mailjet response: {response.StatusCode}");
            Console.WriteLine($"Mailjet body: {response.GetData()}");
        }
    }
}
