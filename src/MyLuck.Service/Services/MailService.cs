namespace MyLuck.Service.Services;

using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.Extensions.Options;

using MimeKit;

using MyLuck.Service.Models;

using System.Threading.Tasks;

public class MailService : IMailService
{
    private readonly EmailSettings _emailSettings;

    public MailService(IOptions<EmailSettings> emailSettings) 
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        MimeMessage email = PrepareEmailMessage(mailRequest);

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _emailSettings.SmtpServer, 
            _emailSettings.Port, 
            SecureSocketOptions.Auto);

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
        email.To.Add(MailboxAddress.Parse(_emailSettings.To));

        return email;
    }
}
