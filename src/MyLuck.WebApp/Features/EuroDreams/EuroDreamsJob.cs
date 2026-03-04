
using Quartz;

using System.Diagnostics;

namespace MyLuck.WebApp.Features.EuroDreams;
internal sealed partial class EuroDreamsJob(ILogger<EuroDreamsJob> logger, IEuroDreamsService euroDreamsService) : IJob
{
    internal static DateTimeOffset? LastExecutionTime;
    
    public async Task Execute(IJobExecutionContext context)
    {
        var timer = Stopwatch.StartNew();
        LogCurrenttimeEurodreamsjobIsRunning(logger, DateTime.Now);

        await euroDreamsService.GetResultsAsync();

        LastExecutionTime = DateTimeOffset.UtcNow;

        timer.Stop();
        LogCurrenttimeEurodreamsjobIsEndingTookDurationMs(logger, DateTime.Now, timer.ElapsedMilliseconds);
    }

    [LoggerMessage(LogLevel.Information, "{CurrentTime} - EuroDreamsJob is running")]
    static partial void LogCurrenttimeEurodreamsjobIsRunning(ILogger<EuroDreamsJob> logger, DateTime currentTime);

    [LoggerMessage(LogLevel.Information, "{CurrentTime} - EuroDreamsJob is ending (took {Duration}ms)")]
    static partial void LogCurrenttimeEurodreamsjobIsEndingTookDurationMs(ILogger<EuroDreamsJob> logger, DateTime currentTime, long duration);
}
