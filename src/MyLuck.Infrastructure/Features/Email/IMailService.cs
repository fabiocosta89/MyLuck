namespace MyLuck.Infrastructure.Features.Email;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
