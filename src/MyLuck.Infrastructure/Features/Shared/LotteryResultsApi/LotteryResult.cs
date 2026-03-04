using System.Text.Json.Serialization;

namespace MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;

public sealed record LotteryDraw(
    [property: JsonPropertyName("draws")] LotteryResult[] Draws);

public sealed record LotteryResult(
    [property: JsonPropertyName("date")] DateOnly Date, 
    [property: JsonPropertyName("numbers")] NumberResult[] Numbers);

public sealed record NumberResult(
    [property: JsonPropertyName("position")] int Position, 
    [property: JsonPropertyName("value")] int Value, 
    [property: JsonPropertyName("special")] bool IsSpecial);