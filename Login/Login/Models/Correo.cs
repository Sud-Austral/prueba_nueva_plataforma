using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace Login.Models
{
    public class Correo
    {
        public static void SendEmailAsync(string correo, string contenido)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("soporte@dataintelligence-group.com", "sud123456789");
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("soporte@dataintelligence-group.com", "Recuperar contraseña"),
                    Subject = "DATAINTELLIGENCE",
                    Body = "<html><head></head><body><p>Correo de Recuperación de Contraseña</p><br/>"+
                    contenido +
                    "</body></html>",
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(correo));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "mail.dataintelligence-group.com",
                    EnableSsl = false,
                    Credentials = credentials
                };

                // Send it...         
                client.Send(mail);
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }

            //return Task.Com;
        }
    }
}