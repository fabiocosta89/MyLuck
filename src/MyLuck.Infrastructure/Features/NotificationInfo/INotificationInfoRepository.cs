namespace MyLuck.Infrastructure.Features.NotificationInfo;

public interface INotificationInfoRepository
{
    Task<IEnumerable<string>> GetActiveEmails(CancellationToken cancellationToken);
}