using Doctorla.Core.InternalDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Communication
{
    public static class EmailSender
    {
        /// <summary>
        /// Sends given email. If emailOptions is not provided, this gets those values from application configuration.
        /// </summary>
        /// <returns></returns>
        public static async Task Send(Email email, EmailSettings emailSettings = null, Attachment attachment = null)
        {
            emailSettings ??= ServiceLocator.AppSettings.EmailSettings;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.FromEmail, "Doctorla"),
                Body = email.Body,
                Subject = email.Subject,
                IsBodyHtml = true,
                Priority = MailPriority.High
            };

            if (attachment != null)
                mailMessage.Attachments.Add(attachment);

            if (email.EmailToList != null)
                foreach (var mail in email.EmailToList)
                {
                    mailMessage.To.Add(mail);
                }

            if (email.EmailCcList != null)
                mailMessage.CC.Add(string.Join(",", email.EmailCcList));

            if (email.EmailBccList != null)
                mailMessage.Bcc.Add(string.Join(",", email.EmailBccList));

            using var smtp = new SmtpClient(emailSettings.SmtpHost, emailSettings.SmtpPort) { EnableSsl = true, UseDefaultCredentials = false, DeliveryMethod = SmtpDeliveryMethod.Network, Credentials = new NetworkCredential(emailSettings.SmtpUserName, emailSettings.SmtpPassword) };

            await smtp.SendMailAsync(mailMessage);
        }
    }
}
