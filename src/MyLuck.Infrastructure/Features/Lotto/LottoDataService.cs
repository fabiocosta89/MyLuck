namespace MyLuck.Infrastructure.Features.Lotto;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MyLuck.Infrastructure.MongoDb;
using MyLuck.Infrastructure.Settings;

using System.Threading.Tasks;

internal class LottoDataService : BaseDataService<Lotto>, ILottoDataService
{
    public LottoDataService(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
        : base(myLuckDatabaseSettings)
    {
    }

    public async Task<Lotto?> GetAsync(string drawId) =>
        await _mongodbCollection
        .Find(lotto => lotto.DrawId == drawId)
        .FirstOrDefaultAsync();

    public async Task<Lotto?> GetByDrawTimeAsync(decimal drawTime) =>
        await _mongodbCollection
        .Find(lotto => lotto.DrawTimeValue == drawTime)
        .FirstOrDefaultAsync();

    public async Task<bool> ExisteAsync(string drawId) => 
        await _mongodbCollection.CountDocumentsAsync(draw => draw.DrawId == drawId) > 0;

    public async Task<bool> ExisteByDrawTimeAsync(decimal drawTime) => 
        await _mongodbCollection
        .CountDocumentsAsync(draw => draw.DrawTimeValue == drawTime) > 0;
}
