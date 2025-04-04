using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyLuck.Infrastructure.MongoDb;
using MyLuck.Infrastructure.Settings;

namespace MyLuck.Infrastructure.Features.NotificationInfo;

public class NotificationInfoRepository(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
    : BaseDataService<NotificationInfo>(myLuckDatabaseSettings), INotificationInfoRepository
{
    public async Task<IEnumerable<NotificationInfo>> GetActiveEmails(CancellationToken cancellationToken)
    {
        var filter = Builders<NotificationInfo>.Filter.Eq(x => x.IsActive, true);

        return await MongodbCollection
            .Find(filter)
            .ToListAsync(cancellationToken);
    }
    
    public async Task AddLotteryKeyAsync(string id, LotteryKey lotteryKey, CancellationToken cancellationToken)
    {
        var filter = Builders<NotificationInfo>.Filter.Eq(x => x.Id, id);
        var update = Builders<NotificationInfo>.Update.AddToSet(x => x.LotteryKey, lotteryKey);
        
        await MongodbCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }
}