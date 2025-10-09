using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProyectoTesis.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER");
        private readonly int _smtpPort = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out var port) ? port : 587;
        private readonly string _smtpUser = Environment.GetEnvironmentVariable("SMTP_USER");
        private readonly string _smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASS");
        private readonly string _fromEmail = Environment.GetEnvironmentVariable("FROM_EMAIL") ?? "marcofabianj@hotmail.com";

        public async Task EnviarCorreoConPdfAsync(string destinatario, string asunto, string cuerpoHtml, byte[] pdfBytes)
        {
            using (var smtp = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtp.Credentials = new NetworkCredential(_smtpUser, _smtpPassword);
                smtp.EnableSsl = true;

                var mensaje = new MailMessage
                {
                    From = new MailAddress(_fromEmail, "Vocacional App"),
                    Subject = asunto,
                    Body = cuerpoHtml,
                    IsBodyHtml = true
                };

                mensaje.To.Add(destinatario);

                if (pdfBytes != null && pdfBytes.Length > 0)
                {
                    var adjunto = new Attachment(
                        new System.IO.MemoryStream(pdfBytes),
                        "ResultadosVocacionales.pdf",
                        "application/pdf"
                    );
                    mensaje.Attachments.Add(adjunto);
                }

                await smtp.SendMailAsync(mensaje);
                Console.WriteLine("Correo enviado correctamente con Brevo SMTP.");
            }
        }
    }
}
