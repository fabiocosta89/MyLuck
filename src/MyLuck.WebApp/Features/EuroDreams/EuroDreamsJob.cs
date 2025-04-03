
using Quartz;

using System.Diagnostics;

namespace MyLuck.WebApp.Features.EuroDreams;
internal class EuroDreamsJob(ILogger<EuroDreamsJob> logger, IEuroDreamsService euroDreamsService) : IJob
{
    internal static DateTimeOffset? LastExecutionTime;
    
    public async Task Execute(IJobExecutionContext context)
    {
        var timer = Stopwatch.StartNew();
        logger.LogInformation("{CurrentTime} - EuroDreamsJob is running", DateTime.Now);

        await euroDreamsService.GetResultsAsync();

        LastExecutionTime = DateTimeOffset.UtcNow;

        timer.Stop();
        logger.LogInformation("{CurrentTime} - EuroDreamsJob is ending (took {Duration}ms)",
            DateTime.Now, 
            timer.ElapsedMilliseconds);
    }
}
