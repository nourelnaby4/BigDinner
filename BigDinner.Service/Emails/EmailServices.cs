using System.Net;
using System.Net.Mail;

namespace BigDinner.Service.Emails;

public class EmailService : IEmailService
{
    private readonly EmailSetting _mailSettings;

    public EmailService(EmailSetting mailSettings)
    {
        _mailSettings = mailSettings;
    }
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var message = new MailMessage()
        {
            From = new MailAddress(_mailSettings.Email!, _mailSettings.DisplayName),
            Body = body,
            Subject = subject,
            IsBodyHtml = true
        };

        message.To.Add(email);

        SmtpClient smtpClient = new SmtpClient(_mailSettings.Host)
        {
            Port = _mailSettings.Port,
            Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(message);

        smtpClient.Dispose();
    }
}
