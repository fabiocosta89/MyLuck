namespace MyLuck.Infrastructure.Features.Email;

public interface IMailService
{
    public Task SendEmailAsync(MailRequest mailRequest);
}
