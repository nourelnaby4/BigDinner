namespace BigDinner.Application.Common.Abstractions.Emails;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string body);
}
