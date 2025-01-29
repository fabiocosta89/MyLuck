namespace MyLuck.WebApp.Features.EuroDreams;

using Microsoft.Extensions.Logging;

using Quartz;

using System.Diagnostics;

internal class EuroDreamsJob(ILogger<EuroDreamsJob> logger, IEuroDreamsService euroDreamsService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var timer = Stopwatch.StartNew();
        logger.LogInformation("{CurrentTime} - EuroDreamsJob is running", DateTime.Now);

        await euroDreamsService.GetResultsAsync();

        timer.Stop();
        logger.LogInformation("{CurrentTime} - EuroDreamsJob is ending (took {Duration}ms)",
            DateTime.Now, 
            timer.ElapsedMilliseconds);
    }
}
