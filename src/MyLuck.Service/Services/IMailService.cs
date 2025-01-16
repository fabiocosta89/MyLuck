namespace MyLuck.Service.Services;

using MyLuck.Service.Models;

using System.Threading.Tasks;

public interface IMailService
{
    public Task SendEmailAsync(MailRequest mailRequest);
}
