namespace MyLuck.Infrastructure.Features.Settings;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MyLuck.Infrastructure.Settings;

internal class SettingsDataService : ISettingsDataService
{
    private readonly IMongoDatabase _mongoDatabase;

    public SettingsDataService(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
    {
        MongoClientSettings settings = MongoClientSettings.FromConnectionString(myLuckDatabaseSettings.Value.ConnectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        MongoClient mongoClient = new(settings);

        _mongoDatabase = mongoClient.GetDatabase(
            myLuckDatabaseSettings.Value.DatabaseName);
    }

    public async Task<EmailSettings> GetEmailSettings()
    {
        IMongoCollection<EmailSettings> collection = _mongoDatabase.GetCollection<EmailSettings>("Settings");

        var filter = Builders<EmailSettings>
            .Filter
            .Eq(
                e => e.Type, 
                SettingTypes.Email.ToString());

        return await collection
            .Find(filter)
            .FirstOrDefaultAsync();

        //var filter = Builders<EmailSettings>.Filter.Eq(e => e.Type.ToString(), SettingTypes.Email.ToString());
        //IAsyncCursor<EmailSettings> result = await collection
        //    .Find(filter).ToCursorAsync();

        //var result2 = await collection
        //    .FindAsync(t => t.Type == SettingTypes.Email.ToString()).;

        //var r = result.ToEnumerable();

        //EmailSettings emailSettings = await collection
        //    .Find(s => s.Type == SettingTypes.Email)
        //    .FirstOrDefaultAsync();

        //return new EmailSettings();
    }
}
