namespace MyLuck.Service.Models;
using System.Text.Json.Serialization;

public class LoterieDraws<T>
{
    [JsonPropertyName("draws")]
    public T[]? Draws { get; set; }
}
