using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace API.Helpers
{
    public class MailHelper
    {
        public static void Send(string subject, string body, string from, string to)
        {
            string username = ConfigurationManager.AppSettings["noReplyMail"];
            string password = ConfigurationManager.AppSettings["noReplyMailPassword"];

            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                Timeout = 100000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(username, password),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage(new MailAddress(from, "APTours - noreply"), new MailAddress(to))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            client.Send(mailMessage);
        }
    }
}