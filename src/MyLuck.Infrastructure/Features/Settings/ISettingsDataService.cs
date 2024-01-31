namespace MyLuck.Infrastructure.Features.Settings;
using System.Threading.Tasks;

public interface ISettingsDataService
{
    Task<EmailSettings> GetEmailSettings();
}
