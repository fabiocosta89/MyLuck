namespace MyLuck.Service.Features.EuroDreams;

using Microsoft.Extensions.Logging;

using Quartz;

using System.Threading.Tasks;

internal class EuroDreamsJob(ILogger<EuroDreamsJob> logger, IEuroDreamsService euroDreamsService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("EuroDreamsJob is running");

        await euroDreamsService.RunAsync();

        logger.LogInformation("EuroDreamsJob is ending");
    }
}
