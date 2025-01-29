namespace MyLuck.WebApp.Features.Email;

public interface IMailService
{
    public Task SendEmailAsync(MailRequest mailRequest);
}
