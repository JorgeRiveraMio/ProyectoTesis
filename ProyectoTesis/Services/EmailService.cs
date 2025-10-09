using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProyectoTesis.Services
{
    public class EmailService
    {
        // Variables de entorno (más seguras que tener las claves en código)
        private readonly string _smtpServer = Environment.GetEnvironmentVariable("BREVO_SMTP_SERVER") ?? "smtp-relay.brevo.com";
        private readonly int _smtpPort = int.TryParse(Environment.GetEnvironmentVariable("BREVO_SMTP_PORT"), out var port) ? port : 587;
        private readonly string _smtpUser = Environment.GetEnvironmentVariable("BREVO_SMTP_USER") ?? "8aa510001@smtp-brevo.com";
        private readonly string _smtpPassword = Environment.GetEnvironmentVariable("BREVO_SMTP_PASS") ?? "";
        private readonly string _fromEmail = Environment.GetEnvironmentVariable("BREVO_FROM_EMAIL") ?? "marcofabianj@hotmail.com";
        private readonly string _fromName = Environment.GetEnvironmentVariable("BREVO_FROM_NAME") ?? "Vocacional App";

        public async Task EnviarCorreoConPdfAsync(string destinatario, string asunto, string cuerpoHtml, byte[] pdfBytes)
        {
            using (var smtp = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtp.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
                smtp.EnableSsl = true;

                var mensaje = new MailMessage
                {
                    From = new MailAddress(_fromEmail, _fromName),
                    Subject = asunto,
                    Body = cuerpoHtml,
                    IsBodyHtml = true
                };

                mensaje.To.Add(destinatario);

                if (pdfBytes != null && pdfBytes.Length > 0)
                {
                    var pdfAdjunto = new Attachment(
                        new System.IO.MemoryStream(pdfBytes),
                        "ResultadosVocacionales.pdf",
                        "application/pdf"
                    );
                    mensaje.Attachments.Add(pdfAdjunto);
                }

                await smtp.SendMailAsync(mensaje);
                Console.WriteLine("Correo enviado correctamente a través de Brevo.");
            }
        }
    }
}
