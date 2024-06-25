using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace AplicacionWeb
{
    public class MailService
    {
        private MailMessage email;
        private SmtpClient server;

        public MailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("f6df9ef8b5495b", "0879c5c6838d4b");
            server.EnableSsl = true;
            server.Port = 2525;
            server.Host = "smtp.mailtrap.io";
        }

        public void armarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@CatalogoArticulosEJ.com");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            //email.Body = "<h1>Reporte de materias a las que se ha inscripto</h1> <br>Hola, te inscribiste.... bla bla";
            email.Body = cuerpo;

        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}