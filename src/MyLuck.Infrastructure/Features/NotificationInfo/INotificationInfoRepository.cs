namespace MyLuck.Infrastructure.Features.NotificationInfo;

public interface INotificationInfoRepository
{
    Task<IEnumerable<NotificationInfo>> GetActiveEmails(CancellationToken cancellationToken);

    Task AddLotteryKeyAsync(string id, LotteryKey lotteryKey, CancellationToken cancellationToken);
}