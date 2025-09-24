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
        return await MongodbCollection
            .Find(x => true)
            .SortByDescending(x => x.DrawDay)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<EuroDreams>> GetAllWithPaginationAsync(int pageNumber, int itemsPerPage, CancellationToken cancellationToken)
    {
        return await MongodbCollection
            .Find(x => true)
            .SortByDescending(x => x.DrawDay)
            .Skip(pageNumber * itemsPerPage)
            .Limit(itemsPerPage)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(string id, int[] numbers, CancellationToken cancellationToken)
    {
        var filter = Builders<EuroDreams>.Filter.Eq(x => x.Id, id);
        var update = Builders<EuroDreams>.Update.Set(x => x.Numbers, numbers);
        
        await MongodbCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task<long> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        return await MongodbCollection.CountDocumentsAsync(x => true, cancellationToken: cancellationToken);
    }
}