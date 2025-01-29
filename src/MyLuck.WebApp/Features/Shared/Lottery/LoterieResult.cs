namespace MyLuck.WebApp.Features.Shared.Lottery;
using System.Text.Json.Serialization;

public class LoterieResult
{
    [JsonPropertyName("primary")]
    public int[]? Primary { get; set; }
}
