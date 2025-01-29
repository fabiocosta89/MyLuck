namespace MyLuck.Infrastructure.Features.EuroDreams;
using Microsoft.Extensions.Options;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

using MyLuck.Infrastructure.MongoDb;
using MyLuck.Infrastructure.Settings;

using System.Threading.Tasks;

public class EuroDreamDataService(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings) 
    : BaseDataService<EuroDream>(myLuckDatabaseSettings), IEuroDreamDataService
{
    public async Task<bool> ExisteByDrawTimeAsync(decimal drawTime) =>
        await _mongodbCollection
            .CountDocumentsAsync(draw => draw.DrawTimeValue == drawTime) > 0;

    public async Task<IEnumerable<EuroDream>> GetAll(CancellationToken cancellationToken)
    {
        var filter = Builders<EuroDream>.Filter.Empty;
        IAsyncCursor<EuroDream> result = await _mongodbCollection
            .Find(filter)
            .ToCursorAsync(cancellationToken);

        return await result.ToListAsync(cancellationToken);
    }
}
