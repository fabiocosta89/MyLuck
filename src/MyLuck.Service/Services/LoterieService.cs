namespace MyLuck.Service.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

internal class LoterieService : ILoterieService
{
    private readonly IHttpClientFactory _httpFactory;

    public LoterieService(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    public async Task<T?> GetResultAsync<T>(string url)
    {
        using HttpClient httpClient = _httpFactory.CreateClient();

        HttpResponseMessage response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }

        return default;
    }
}
