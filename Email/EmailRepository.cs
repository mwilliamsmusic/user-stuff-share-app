using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_stuff_share_app.Email
{
        public class EmailRpository: IEmailRepository

    {
        
        private MailKitEmailSenderOptions Options;
        public EmailRpository(IOptions<MailKitEmailSenderOptions> options)
            {
                Options = options.Value;
            }


            public Task SendEmailAsync(string email, string subject, string message)
            {
                return Execute(email, subject, message);
            }

            private Task Execute(string to, string subject, string message)
            {
                // create message
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(Options.Sender_EMail);
                if (!string.IsNullOrEmpty(Options.Sender_Name))
                    email.Sender.Name = Options.Sender_Name;
                email.From.Add(email.Sender);
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = message };
     

                // send email
            using (var smtp = new SmtpClient())
                {
                    smtp.Connect(Options.Host_Address, Options.Host_Port, Options.Host_SecureSocketOptions);
                    smtp.Authenticate(Options.Host_Username, Options.Host_Password);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }

                return Task.FromResult(true);
            }
        }
    
}
