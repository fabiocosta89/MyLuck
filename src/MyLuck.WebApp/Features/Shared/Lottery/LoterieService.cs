namespace MyLuck.WebApp.Features.Shared.Lottery;

using System.Net.Http;
using System.Net.Http.Json;

public class LoterieService : ILoterieService
{
    private readonly IHttpClientFactory _httpFactory;

    public LoterieService(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    public async Task<T?> GetResultAsync<T>(string url)
    {
        using var httpClient = _httpFactory.CreateClient();

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }

        return default;
    }
}
