using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyLuck.Infrastructure.MongoDb;
using MyLuck.Infrastructure.Settings;

namespace MyLuck.Infrastructure.Features.NotificationInfo;

public class NotificationInfoRepository(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
    : BaseDataService<NotificationInfo>(myLuckDatabaseSettings), INotificationInfoRepository
{
    public async Task<IEnumerable<string>> GetActiveEmails(CancellationToken cancellationToken)
    {
        var filter = Builders<NotificationInfo>.Filter.Eq(x => x.IsActive, true);

        return await MongodbCollection
            .Find(filter)
            .Project(x => x.Email)
            .ToListAsync(cancellationToken);
    }
}