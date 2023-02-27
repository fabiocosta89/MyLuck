namespace MyLuck.Service.Services;
using System.Threading.Tasks;

internal interface ILoterieService
{
    public Task<T?> GetResultAsync<T>(string url);
}
