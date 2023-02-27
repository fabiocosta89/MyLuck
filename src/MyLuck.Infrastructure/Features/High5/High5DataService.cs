namespace MyLuck.Infrastructure.Features.High5;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MyLuck.Infrastructure.MongoDb;
using MyLuck.Infrastructure.Settings;

internal class High5DataService : BaseDataService<High5>, IHigh5DataService
{
    public High5DataService(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
        : base(myLuckDatabaseSettings)
    {
    }

    public async Task<High5?> GetAsync(string drawId) =>
        await _mongodbCollection.Find(high5 => high5.DrawId == drawId).FirstOrDefaultAsync();

    public async Task<bool> ExisteAsync(string drawId) => await _mongodbCollection
        .CountDocumentsAsync(draw => draw.DrawId == drawId) > 0;
}
