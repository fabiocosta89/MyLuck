namespace MyLuck.Infrastructure.Features.EuroDreams;

public interface IEuroDreamsRepository
{
    Task<bool> ExistByDrawTimeAsync(DateOnly drawDay, CancellationToken cancellationToken);
    
    Task CreateAsync(EuroDreams item, CancellationToken cancellationToken);

    Task<IEnumerable<EuroDreams>> GetAllAsync(CancellationToken cancellationToken);

    Task<IEnumerable<EuroDreams>> GetAllWithPaginationAsync(int pageNumber, int itemsPerPage,
        CancellationToken cancellationToken);

    Task UpdateAsync(string id, int[] numbers, CancellationToken cancellationToken);
    
    Task<long> GetTotalCountAsync(CancellationToken cancellationToken);
}