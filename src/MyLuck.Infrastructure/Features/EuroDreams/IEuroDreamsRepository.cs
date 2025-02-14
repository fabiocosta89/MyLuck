namespace MyLuck.Infrastructure.Features.EuroDreams;

public interface IEuroDreamsRepository
{
    Task<bool> ExistByDrawTimeAsync(DateTimeOffset drawTime, CancellationToken cancellationToken);
    
    Task CreateAsync(EuroDreams item, CancellationToken cancellationToken);

    Task<IEnumerable<EuroDreams>> GetAll(CancellationToken cancellationToken);
}