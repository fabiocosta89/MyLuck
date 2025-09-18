using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyLuck.Infrastructure.MongoDb;
using MyLuck.Infrastructure.Settings;

namespace MyLuck.Infrastructure.Features.EuroDreams;

public class EuroDreamsRepository(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
    : BaseDataService<EuroDreams>(myLuckDatabaseSettings), IEuroDreamsRepository
{
    public async Task<bool> ExistByDrawTimeAsync(DateOnly drawDay, CancellationToken cancellationToken) =>
        await MongodbCollection
            .CountDocumentsAsync(draw => draw.DrawDay == drawDay, cancellationToken: cancellationToken) > 0;

    public async Task<IEnumerable<EuroDreams>> GetAllAsync(CancellationToken cancellationToken)
    {
        var filter = Builders<EuroDreams>.Filter.Empty;
        using IAsyncCursor<EuroDreams> result = await MongodbCollection
            .Find(filter)
            .SortByDescending(x => x.DrawDay)
            .ToCursorAsync(cancellationToken);

        return await result.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(string id, int[] numbers, CancellationToken cancellationToken)
    {
        var filter = Builders<EuroDreams>.Filter.Eq(x => x.Id, id);
        var update = Builders<EuroDreams>.Update.Set(x => x.Numbers, numbers);
        
        await MongodbCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }
}