using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

using MyLuck.Service.Features.EuroDreams;

namespace MyLuck.Functions
{
    public class CheckResults(ILoggerFactory loggerFactory, IEuroDreamsService euroDreamsService)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<CheckResults>();

        [Function(nameof(CheckResults))]
        public async Task Run([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation("C# Timer trigger function executed at: {Time}", DateTime.Now);

            await euroDreamsService.RunAsync();

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation("Next timer schedule at: {NextTime}", myTimer.ScheduleStatus.Next);
            }
        }
    }
}
