namespace MyLuck.Service.Features.Lotto;
using MyLuck.Service.Models;

using System.Text.Json.Serialization;

public class LottoResult : LoterieResult
{
    [JsonPropertyName("secondary")]
    public int[]? Secondary { get; set; }
}
