using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Mailer
{
    public interface IMailer
    {
        Task SendEmailAsyc(string email, string recieverName, string subject, string body);
       
        
    }
    class Mailer : IMailer
    {

        private readonly SmtpSettings _smtpSettings;
        private readonly IWebHostEnvironment _env; 
        public Mailer(IOptions<SmtpSettings> smtpSettings, IWebHostEnvironment env)
        {
            _env = env;
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsyc(string email,string recieverName, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress(recieverName,email));
                message.Subject = subject;
                message.Body = new TextPart("html") { Text = body };
                using(var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    if(_env.IsDevelopment())
                    {
                        await client.ConnectAsync(_smtpSettings.Server, 465,true);
                    }
                    else
                    {
                        await client.ConnectAsync(_smtpSettings.Server);
                    }
                    await client.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch(Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
