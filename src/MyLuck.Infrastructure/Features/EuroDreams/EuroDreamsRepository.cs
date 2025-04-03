using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyLuck.Infrastructure.MongoDb;
using MyLuck.Infrastructure.Settings;

namespace MyLuck.Infrastructure.Features.EuroDreams;

public class EuroDreamsRepository(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
    : BaseDataService<EuroDreams>(myLuckDatabaseSettings), IEuroDreamsRepository
{
    public async Task<bool> ExistByDrawTimeAsync(DateTimeOffset drawTime, CancellationToken cancellationToken) =>
        await MongodbCollection
            .CountDocumentsAsync(draw => draw.DrawTime == drawTime, cancellationToken: cancellationToken) > 0;

    public async Task<IEnumerable<EuroDreams>> GetAll(CancellationToken cancellationToken)
    {
        var filter = Builders<EuroDreams>.Filter.Empty;
        using IAsyncCursor<EuroDreams> result = await MongodbCollection
            .Find(filter)
            .SortByDescending(x => x.DrawTime)
            .ToCursorAsync(cancellationToken);

        return await result.ToListAsync(cancellationToken);
    }
}