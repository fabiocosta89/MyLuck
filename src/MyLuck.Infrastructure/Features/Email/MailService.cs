using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MyLuck.Infrastructure.Features.Email;

public class MailService : IMailService
{
    private readonly MailSettings _emailSettings;

    public MailService(IOptions<MailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        MimeMessage email = PrepareEmailMessage(mailRequest);

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _emailSettings.SmtpServer,
            _emailSettings.Port);

        await smtp.AuthenticateAsync(
            _emailSettings.UserName,
            _emailSettings.Password);

        await smtp.SendAsync(email);
    }

    private MimeMessage PrepareEmailMessage(MailRequest mailRequest)
    {
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_emailSettings.From),
            Subject = mailRequest.Subject,
            Body = new BodyBuilder
            {
                HtmlBody = mailRequest.Body
            }.ToMessageBody()
        };
        email.From.Add(MailboxAddress.Parse(_emailSettings.From));
        email.To.Add(MailboxAddress.Parse(mailRequest.EmailTo));

        return email;
    }
}
