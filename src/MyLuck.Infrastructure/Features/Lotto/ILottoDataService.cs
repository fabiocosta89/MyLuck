namespace MyLuck.Infrastructure.Features.Lotto;
using System.Threading.Tasks;

public interface ILottoDataService
{
    Task<Lotto?> GetAsync(string drawId);

    Task<Lotto?> GetByDrawTimeAsync(decimal drawTime);

    Task<IEnumerable<Lotto>> GetAll();

    Task<bool> ExisteAsync(string drawId);

    Task<bool> ExisteByDrawTimeAsync(decimal drawTime);

    Task CreateAsync(Lotto item);
}
