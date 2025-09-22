using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;
public sealed class LotteryResultsApi : ILotteryResultsApi
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly IOptions<LotteryApiSettings> _lotteryApiSettings;
    
    public LotteryResultsApi(IHttpClientFactory httpFactory, IOptions<LotteryApiSettings> lotteryApiSettings)
    {
        _httpFactory = httpFactory;
        _lotteryApiSettings = lotteryApiSettings;
    }

    public async Task<LotteryResult[]?> GetEuroDreamsResults(CancellationToken cancellationToken = default)
    {
        using var httpClient = _httpFactory.CreateClient();
        AddHeaders(httpClient);

        return await httpClient.GetFromJsonAsync<LotteryResult[]>(_lotteryApiSettings.Value.EuroDreams, cancellationToken);
    }
    
    public async Task<LotteryResult[]?> GetEuroDreamsResultsWithOffset(int offset, CancellationToken cancellationToken = default)
    {
        using var httpClient = _httpFactory.CreateClient();
        AddHeaders(httpClient);

        return await httpClient.GetFromJsonAsync<LotteryResult[]>($"{_lotteryApiSettings.Value.EuroDreams}?offset={offset}", cancellationToken);
    }
    
    private void AddHeaders(HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Add("X-API-Token", _lotteryApiSettings.Value.ApiKey);
    }
}