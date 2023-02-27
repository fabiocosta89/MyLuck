namespace MyLuck.Service.Models;
using System.Text.Json.Serialization;

public class LoterieResult
{
    [JsonPropertyName("primary")]
    public int[]? Primary { get; set; }
}
