namespace MyLuck.Infrastructure.MongoDb;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MyLuck.Infrastructure.Settings;

public abstract class BaseDataService<T>
{
    protected readonly IMongoCollection<T> _mongodbCollection;

    private protected BaseDataService(IOptions<MyLuckDatabaseSettings> myLuckDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            myLuckDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            myLuckDatabaseSettings.Value.DatabaseName);

        _mongodbCollection = mongoDatabase.GetCollection<T>(typeof(T).Name);
    }

    /// <summary>
    /// Add a new object
    /// </summary>
    /// <param name="item">new item to add</param>
    /// <returns></returns>
    public async Task CreateAsync(T item) => await _mongodbCollection.InsertOneAsync(item);
}
