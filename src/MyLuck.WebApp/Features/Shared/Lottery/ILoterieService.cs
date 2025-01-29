namespace MyLuck.WebApp.Features.Shared.Lottery;

public interface ILoterieService
{
    public Task<T?> GetResultAsync<T>(string url);
}
