using System;
using System.Net;
using System.Net.Mail;
using Infodoctor.BL.DtoModels;
using Infodoctor.BL.Interfaces;

namespace Infodoctor.BL.Services
{
    public class MailService : IMailService
    {
        public void Send(DtoMailMessage mail, DtoMailServiceConfiguration conf)
        {
            if (mail == null)
                throw new ArgumentNullException(nameof(mail));
            if (conf == null)
                throw new ArgumentNullException(nameof(conf));

            var client = new SmtpClient
            {
                Host = conf.SmtpServer,
                Port = conf.SmtpPort,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Credentials = new NetworkCredential(conf.Email, conf.Password)
            };

            var message = new MailMessage()
            {
                From = new MailAddress(conf.Email),
                To = { new MailAddress(mail.SendTo) },
                Subject = mail.Subject,
                Body = mail.Body
            };

            client.Send(message);
        }
    }
}
