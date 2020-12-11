using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cnpm.Common
{
    public static class SendMail
    {
        public static void sendMail(string subject, string toAddress, string text)
        {
            // Instantiate mimemessage
            var message = new MimeMessage();

            // From address
            message.From.Add(new MailboxAddress("VATVA SHOP", "vatvashop20@gmail.com"));

            // To address
            message.To.Add(new MailboxAddress("Vatva Shop", toAddress));

            // Subject
            message.Subject = subject;

            // Body
            message.Body = new TextPart("plain")
            {
                Text = text
            };

            // configure and send mail
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("vatvashop20@gmail.com", "3117410300");

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}
