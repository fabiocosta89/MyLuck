namespace MyLuck.Infrastructure.MongoDb;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using Settings;

public abstract class BaseDataService<T>
{
    protected readonly IMongoCollection<T> MongodbCollection;

    private protected BaseDataService(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            myLuckDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            myLuckDatabaseSettings.Value.DatabaseName);

        MongodbCollection = mongoDatabase.GetCollection<T>(typeof(T).Name);
    }

    public async Task CreateAsync(T item, CancellationToken cancellationToken) => await MongodbCollection.InsertOneAsync(item, cancellationToken: cancellationToken);
}
