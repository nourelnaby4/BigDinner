namespace BigDinner.Application.Common.Abstractions;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string body);
}
