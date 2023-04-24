namespace MyLuck.Infrastructure.Features.Key;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IKeyDataService
{
    Task<IEnumerable<Key>> GetAsync(string gameName);
}
