namespace MyLuck.Service.Services;

using MyLuck.Service.Models;

using System.Threading.Tasks;

internal interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
