namespace MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;

public interface ILotteryResultsApi
{
    Task<LotteryResult[]?> GetEuroDreamsResults(CancellationToken cancellationToken = default);
}