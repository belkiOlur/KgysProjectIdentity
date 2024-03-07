using KgysProjectIdentity.Core.OptionsModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace KgysProjectIdentity.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostEnvironment _hostEnvironment;

        public EmailService(IOptions<EmailSettings> options, IHostEnvironment hostEnvironment)
        {
            _emailSettings = options.Value;
            _hostEnvironment = hostEnvironment;
        }

        public async Task SendEmail(string MailBody, string ToEmails)
        {
            var smptClient = new SmtpClient();

            smptClient.Host = _emailSettings.Host;
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smptClient.EnableSsl = true;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailSettings.Email);
            //mailMessage.To.Add(ToEmails);
            //mailMessage.Bcc.Add(ToEmails);
            mailMessage.To.Add(ToEmails);


            mailMessage.Subject = "Yeni BITHAP Şifre Değiştirme Güvenlik Kodu";
            mailMessage.Body = "Güvenlik Kodunuz = "+MailBody;

            mailMessage.IsBodyHtml = true;


            await smptClient.SendMailAsync(mailMessage);



        }
        

    }
}
