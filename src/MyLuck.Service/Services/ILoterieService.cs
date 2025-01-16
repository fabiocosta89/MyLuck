namespace MyLuck.Service.Services;
using System.Threading.Tasks;

public interface ILoterieService
{
    public Task<T?> GetResultAsync<T>(string url);
}
