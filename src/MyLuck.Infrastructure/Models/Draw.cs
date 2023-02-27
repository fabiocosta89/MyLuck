namespace MyLuck.Infrastructure.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;

public abstract class Draw
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? DrawId { get; set; }
    public DateTime DrawTime { get; set; }
    public decimal? DrawTimeValue { get; set; }
}
