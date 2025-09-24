using System.Net.Mail;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace ProyectoTesis.Services
{
    public class EmailService
    {
        private readonly string _from = "marcofabianjz@gmail.com";
        private readonly string _password = "vxrgdmooedyigrrd"; // Usa contraseña de aplicación si es Gmail

        public async Task EnviarCorreoConPdfAsync(string destinatario, string asunto, string cuerpoHtml, byte[] pdfBytes)
        {
            var mensaje = new MailMessage();
            mensaje.From = new MailAddress(_from, "Vocacional App");
            mensaje.To.Add(destinatario);
            mensaje.Subject = asunto;
            mensaje.Body = cuerpoHtml;
            mensaje.IsBodyHtml = true;

            // Adjuntar PDF
            mensaje.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), "ResultadosVocacionales.pdf"));

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(_from, _password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mensaje);
            }
        }
    }
}
