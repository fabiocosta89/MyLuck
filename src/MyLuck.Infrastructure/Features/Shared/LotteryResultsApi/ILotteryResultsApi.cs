namespace MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;

public interface ILotteryResultsApi
{
    Task<LotteryDraw?> GetEuroDreamsResults(CancellationToken cancellationToken = default);

    Task<LotteryDraw?> GetEuroDreamsResultsWithOffset(int offset, CancellationToken cancellationToken = default);
}