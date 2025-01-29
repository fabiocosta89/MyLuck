namespace MyLuck.WebApp.Features.EuroDreams;
using MyLuck.WebApp.Features.Shared.Lottery;

using System.Text.Json.Serialization;

public class EuroDreamsResult : LoterieResult
{
    [JsonPropertyName("secondary")]
    public int[]? Secondary { get; set; }
}
