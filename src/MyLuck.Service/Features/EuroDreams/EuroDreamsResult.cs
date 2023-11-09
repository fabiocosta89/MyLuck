namespace MyLuck.Service.Features.EuroDreams;
using MyLuck.Service.Models;

using System.Text.Json.Serialization;

public class EuroDreamsResult : LoterieResult
{
    [JsonPropertyName("secondary")]
    public int[]? Secondary { get; set; }
}
