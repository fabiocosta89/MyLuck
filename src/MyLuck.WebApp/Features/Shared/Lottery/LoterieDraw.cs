namespace MyLuck.WebApp.Features.Shared.Lottery;
using System.Text.Json.Serialization;

public class LoterieDraw<T>
{
    [JsonPropertyName("drawId")]
    public string DrawId { get; set; } = string.Empty;

    [JsonPropertyName("drawTime")]
    public decimal DrawTime { get; set; }

    [JsonPropertyName("results")]
    public T[]? Results { get; set; }
}
