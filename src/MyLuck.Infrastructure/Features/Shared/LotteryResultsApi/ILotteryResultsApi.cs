namespace MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;

public interface ILotteryResultsApi
{
    Task<LotteryResult[]?> GetEuroDreamsResults(CancellationToken cancellationToken = default);

    Task<LotteryResult[]?> GetEuroDreamsResultsWithOffset(int offset, CancellationToken cancellationToken = default);
}