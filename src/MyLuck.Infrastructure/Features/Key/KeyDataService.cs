namespace MyLuck.Infrastructure.Features.Key;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MyLuck.Infrastructure.MongoDb;
using MyLuck.Infrastructure.Settings;

using System.Collections.Generic;
using System.Threading.Tasks;

internal class KeyDataService : BaseDataService<Key>, IKeyDataService
{
    public KeyDataService(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings) 
        : base(myLuckDatabaseSettings)
    {
    }

    public async Task<IEnumerable<Key>> GetAsync(string gameName)
    {
        return await _mongodbCollection
            .Find(filter: key => key.Game == gameName).ToListAsync();
    }
}
