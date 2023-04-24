namespace MyLuck.Infrastructure.Features.Key;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Key
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Game { get; set; }

    public int[]? Numbers { get; set; }

    public int[]? SpecialNumbers { get; set; }
}
