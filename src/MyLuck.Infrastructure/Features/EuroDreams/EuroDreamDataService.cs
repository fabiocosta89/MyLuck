namespace MyLuck.Infrastructure.Features.EuroDreams;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MyLuck.Infrastructure.MongoDb;
using MyLuck.Infrastructure.Settings;

using System.Threading.Tasks;

internal class EuroDreamDataService : BaseDataService<EuroDream>, IEuroDreamDataService
{
    public EuroDreamDataService(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings) 
        : base(myLuckDatabaseSettings)
    {
    }

    public async Task<bool> ExisteByDrawTimeAsync(decimal drawTime) =>
        await _mongodbCollection
            .CountDocumentsAsync(draw => draw.DrawTimeValue == drawTime) > 0;
}
