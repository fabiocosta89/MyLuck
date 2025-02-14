using MyLuck.Infrastructure.Models;

namespace MyLuck.Infrastructure.Features.EuroDreams;
using System.Threading.Tasks;

public interface IEuroDreamDataService
{
    Task<bool> ExisteByDrawTimeAsync(decimal drawTime);
    
    Task CreateAsync(EuroDream item, CancellationToken cancellationToken);

    Task<IEnumerable<EuroDream>> GetAll(CancellationToken cancellationToken);
}
