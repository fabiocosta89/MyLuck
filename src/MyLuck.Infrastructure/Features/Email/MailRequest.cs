namespace MyLuck.Infrastructure.Features.Email;
public sealed class MailRequest
{
    public string EmailTo { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
