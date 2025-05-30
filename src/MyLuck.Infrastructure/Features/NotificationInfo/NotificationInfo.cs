using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyLuck.Infrastructure.Features.NotificationInfo;

public sealed class NotificationInfo(string email)
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; }
    
    public string Email { get; private set; } = email;
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public bool IsActive { get; private set; } = true;
    public LotteryKey[] LotteryKey { get; private set; } = [];
}

public sealed record LotteryKey(
    int[] Numbers,
    int[] SpecialNumbers,
    [property: BsonRepresentation(BsonType.String)]
    LotteryType LotteryType);

public enum LotteryType
{
    EuroDreams,
}