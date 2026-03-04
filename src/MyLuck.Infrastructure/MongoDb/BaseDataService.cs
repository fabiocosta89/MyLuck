using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MyLuck.Infrastructure.Settings;

namespace MyLuck.Infrastructure.MongoDb;

public abstract class BaseDataService<T>
{
    protected readonly IMongoCollection<T> MongodbCollection;

    private protected BaseDataService(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
    {
#pragma warning disable CA2000
        var mongoClient = new MongoClient(
            myLuckDatabaseSettings.Value.ConnectionString);
#pragma warning restore CA2000

        IMongoDatabase? mongoDatabase = mongoClient.GetDatabase(
            myLuckDatabaseSettings.Value.DatabaseName);

        MongodbCollection = mongoDatabase.GetCollection<T>(typeof(T).Name);
    }

    public async Task CreateAsync(T item, CancellationToken cancellationToken) => await MongodbCollection.InsertOneAsync(item, cancellationToken: cancellationToken);
}
