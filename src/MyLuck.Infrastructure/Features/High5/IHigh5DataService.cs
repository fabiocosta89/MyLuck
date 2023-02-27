namespace MyLuck.Infrastructure.Features.High5;

using System.Threading.Tasks;

public interface IHigh5DataService
{
    Task<High5?> GetAsync(string drawId);

    Task<bool> ExisteAsync(string drawId);

    Task CreateAsync(High5 item);
}
