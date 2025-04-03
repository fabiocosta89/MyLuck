namespace MyLuck.WebApp.Features.EuroDreams;

public interface IEuroDreamsService
{
    public Task GetResultsAsync(CancellationToken cancellationToken = default);
}
