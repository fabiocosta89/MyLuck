using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyLuck.Infrastructure.Models;

public abstract class DrawResult(int[] numbers, int[] specialNumbers, DateOnly drawDay)
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; }
    
    public DateOnly DrawDay { get; private set; } = drawDay;

    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

    public int[] Numbers { get; set; } = numbers;

    public int[] SpecialNumbers { get; private set; } = specialNumbers;
}