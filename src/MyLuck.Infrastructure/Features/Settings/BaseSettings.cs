namespace MyLuck.Infrastructure.Features.Settings;
using MongoDB.Bson;

public abstract class BaseSettings
{
    public ObjectId Id { get; set; }
    public string Type { get; set; } = string.Empty;
}
