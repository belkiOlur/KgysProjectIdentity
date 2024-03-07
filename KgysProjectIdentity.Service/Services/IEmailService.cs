namespace KgysProjectIdentity.Service.Services
{
    public interface IEmailService
    {
        Task SendEmail(string MailBody, string ToEmails);
       
    }
}