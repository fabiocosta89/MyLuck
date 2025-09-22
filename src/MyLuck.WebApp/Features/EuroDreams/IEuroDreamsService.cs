namespace MyLuck.WebApp.Features.EuroDreams;

public interface IEuroDreamsService
{
    Task GetResultsAsync(CancellationToken cancellationToken = default);

    Task GetOldResultsAsync(CancellationToken cancellationToken = default);
}
