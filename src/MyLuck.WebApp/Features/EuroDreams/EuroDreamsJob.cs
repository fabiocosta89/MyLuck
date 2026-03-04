
using Quartz;

using System.Diagnostics;

namespace MyLuck.WebApp.Features.EuroDreams;
internal sealed partial class EuroDreamsJob(ILogger<EuroDreamsJob> logger, IEuroDreamsService euroDreamsService) : IJob
{
    private static DateTimeOffset? _lastExecutionTime;
    
    public async Task Execute(IJobExecutionContext context)
    {
        var timer = Stopwatch.StartNew();
        LogCurrenttimeEurodreamsjobIsRunning(logger, DateTime.Now);

        await euroDreamsService.GetResultsAsync();

        SetLastExecutionTime(DateTimeOffset.UtcNow);

        timer.Stop();
        LogCurrenttimeEurodreamsjobIsEndingTookDurationMs(logger, DateTime.Now, timer.ElapsedMilliseconds);
    }
    
    internal static DateTimeOffset? GetLastExecutionTime() => _lastExecutionTime;

    private static void SetLastExecutionTime(DateTimeOffset time) => _lastExecutionTime = time;

    [LoggerMessage(LogLevel.Information, "{CurrentTime} - EuroDreamsJob is running")]
    static partial void LogCurrenttimeEurodreamsjobIsRunning(ILogger<EuroDreamsJob> logger, DateTime currentTime);

    [LoggerMessage(LogLevel.Information, "{CurrentTime} - EuroDreamsJob is ending (took {Duration}ms)")]
    static partial void LogCurrenttimeEurodreamsjobIsEndingTookDurationMs(ILogger<EuroDreamsJob> logger, DateTime currentTime, long duration);
}
